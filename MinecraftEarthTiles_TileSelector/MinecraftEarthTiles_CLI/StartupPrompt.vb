﻿Imports System.ComponentModel
Imports System.IO
Imports System.Threading
Imports MinecraftEarthTiles_Core

Module StartupPrompt

    Dim MyGenerationWorker As GenerationWorker

    Dim MyGenerationType As String = "full"

    Sub Main(args As String())
        AddHandler AppDomain.CurrentDomain.ProcessExit, AddressOf OnClose
        AddHandler Console.CancelKeyPress, AddressOf OnClose

        Dim assemblyVersion = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version
        Dim coreType As Type = ClassWorker.GetTilesSettings.GetType
        Dim coreVersion = System.Reflection.Assembly.GetAssembly(coreType).GetName.Version
        Console.WriteLine($"CLI Version: v{assemblyVersion.Major}.{assemblyVersion.Minor}.{assemblyVersion.Build}, Core: v{coreVersion.Major}.{coreVersion.Minor}.{coreVersion.Build}")
        Console.WriteLine("Copyright © DerMattinger 2020 - 2023")
        Console.WriteLine("OSM Data: © OpenStreetMap Contributors: https://www.openstreetmap.org/copyright")

        If (args.Length = 0) Then
            Console.WriteLine("Missing arguments")
            Console.WriteLine("")
            Console.WriteLine("Available arguments:")
            Console.WriteLine("Mandatory:")
            Console.WriteLine(" -world_settings <path to your settings.xml>")
            Console.WriteLine(" -tiles_settings <path to your tiles_settings.xml>")
            Console.WriteLine(" -selection <path to your selection.xml>")
            Console.WriteLine("Optional:")
            Console.WriteLine(" -generation <type of generation>")
            Console.WriteLine("  Available types of generation:")
            Console.WriteLine("  full, batch, osm, images, worldpainter, combine, cleanup")
            Console.WriteLine("  The standard type of generation is full")
            Console.WriteLine("")
            Console.WriteLine("Press any key to continue...")
            Console.ReadKey()
            End
        Else
            For i As Integer = 0 To args.Length - 1
                Select Case args(i)
                    Case "-world_settings"
                        If i + 1 <= args.Length Then
                            If File.Exists(args(i + 1)) Then
                                Try
                                    ClassWorker.SetWorldSettings(ClassWorker.LoadWorldSettingsFromFile(args(i + 1)))
                                Catch ex As Exception
                                    ShowError(ex.Message)
                                End Try
                            Else
                                ShowError($"File ""{args(i + 1)}"" not found")
                            End If
                        Else
                            ShowError("Missing argument for -world_settings")
                        End If
                    Case "-tiles_settings"
                        If i + 1 <= args.Length Then
                            If File.Exists(args(i + 1)) Then
                                Try
                                    ClassWorker.SetTilesSettings(ClassWorker.LoadTilesSettingsFromFile(args(i + 1)))
                                Catch ex As Exception
                                    ShowError(ex.Message)
                                End Try
                            Else
                                ShowError($"File ""{args(i + 1)}"" not found")
                            End If
                        Else
                            ShowError("Missing argument for -tiles_settings")
                        End If
                    Case "-selection"
                        If i + 1 <= args.Length Then
                            If File.Exists(args(i + 1)) Then
                                Try
                                    ClassWorker.SetSelection(ClassWorker.LoadSelectionFromFile(args(i + 1)))
                                Catch ex As Exception
                                    ShowError(ex.Message)
                                End Try
                            Else
                                ShowError($"File ""{args(i + 1)}"" not found")
                            End If
                        Else
                            ShowError("Missing argument for -selection")
                        End If
                    Case "-generation"
                        If i + 1 <= args.Length Then
                            Select Case args(i + 1)
                                Case "full"
                                    MyGenerationType = "full"
                                Case "batch"
                                    MyGenerationType = "batch"
                                Case "osm"
                                    MyGenerationType = "osm"
                                Case "images"
                                    MyGenerationType = "images"
                                Case "worldpainter"
                                    MyGenerationType = "worldpainter"
                                Case "combine"
                                    MyGenerationType = "combine"
                                Case "cleanup"
                                    MyGenerationType = "cleanup"
                            End Select
                        Else
                            ShowError("Missing argument for -generation")
                        End If
                End Select
            Next
            Console.WriteLine("Loading completed")
        End If
        Check()
        Console.WriteLine("Starting generation")
        ClassWorker.SaveWorldSettingsToFile(ClassWorker.GetWorldSettings, ClassWorker.GetTilesSettings.PathToScriptsFolder & "/settings.xml")
        ClassWorker.SaveTilesSettingsToFile(ClassWorker.GetTilesSettings, ClassWorker.GetTilesSettings.PathToScriptsFolder & "/tiles_settings.xml")
        ClassWorker.SaveSelectionToFile(ClassWorker.GetSelection, ClassWorker.GetTilesSettings.PathToScriptsFolder & "/selection.xml")
        MyGenerationWorker = New GenerationWorker
        AddHandler MyGenerationWorker.PropertyChanged, New PropertyChangedEventHandler(AddressOf PropertyChanged)
        MyGenerationWorker.CreateGeneration()
        MyGenerationWorker.tilesReady = 0
        MyGenerationWorker.startTime = DateTime.Now
        MyGenerationWorker.keepRunning = True
        Select Case MyGenerationType
            Case "full"
                MyGenerationWorker.neuerthread = New Thread(AddressOf MyGenerationWorker.Tile_Generation)
                MyGenerationWorker.neuerthread.Start()
            Case "batch"
                MyGenerationWorker.CleanupFinalBatchExport()
                MyGenerationWorker.OsmbatExportPrepare()
                For Each Tile In ClassWorker.GetSelection.TilesList
                    If Tile.Contains("x") Then
                        MyGenerationWorker.VoidScriptBatchExport(Tile)
                    Else
                        MyGenerationWorker.OsmbatBatchExport(Tile)
                        MyGenerationWorker.QgisRepairBatchExport(Tile)
                        MyGenerationWorker.QgisBatchExport(Tile)
                        MyGenerationWorker.TartoolBatchExport(Tile)
                        MyGenerationWorker.GdalBatchExport(Tile)
                        MyGenerationWorker.ImageMagickBatchExport(Tile)
                        MyGenerationWorker.WpScriptBatchExport(Tile, False)
                        MyGenerationWorker.WpScriptBatchExport(Tile, True)
                        MyGenerationWorker.MinutorRenderExort(Tile)
                        MyGenerationWorker.CleanupBatchExport(Tile)
                    End If
                Next
                MyGenerationWorker.CombineBatchExport()
            Case "osm"
                MyGenerationWorker.neuerthread = New Thread(AddressOf MyGenerationWorker.OsmOnly_Generation)
                MyGenerationWorker.neuerthread.Start()
            Case "images"
                MyGenerationWorker.neuerthread = New Thread(AddressOf MyGenerationWorker.ImagesOnly_Generation)
                MyGenerationWorker.neuerthread.Start()
            Case "worldpainter"
                MyGenerationWorker.neuerthread = New Thread(AddressOf MyGenerationWorker.WorldPainterOnly_Generation)
                MyGenerationWorker.neuerthread.Start()
            Case "combine"
                MyGenerationWorker.neuerthread = New Thread(AddressOf MyGenerationWorker.CombineOnly_Generation)
                MyGenerationWorker.neuerthread.Start()
            Case "cleanup"
                MyGenerationWorker.neuerthread = New Thread(AddressOf MyGenerationWorker.CleanupOnly_Generation)
                MyGenerationWorker.neuerthread.Start()
        End Select
        If MyGenerationWorker.keepRunning = False Then
            ClassWorker.CreateDebugZip()
        End If
    End Sub

    Sub ShowError(message As String)
        Console.WriteLine(message)
        Console.WriteLine("Press any key to continue...")
        Console.ReadKey()
        End
    End Sub

    Private Sub Check()
        Console.WriteLine("Checking settings")
        If ClassWorker.GetTilesSettings.PathToMagick = "" Then
            ShowError("Path to ImageMagick is not correct.")
        ElseIf ClassWorker.GetTilesSettings.PathToQGIS = "" Then
            ShowError("Path to QGIS is not correct.")
        ElseIf ClassWorker.GetTilesSettings.PathToScriptsFolder = "" Then
            ShowError("Path to script folger is not correct.")
        ElseIf ClassWorker.GetTilesSettings.PathToWorldPainterFolder = "" Then
            ShowError("Path to WorldPainter is not correct.")
        ElseIf ClassWorker.GetWorldSettings.PathToPBF = "" _
            And ClassWorker.GetWorldSettings.geofabrik = True _
            And ClassWorker.GetTilesSettings.reUsePbfFile = False _
            And ClassWorker.GetTilesSettings.reUseOsmFiles = False _
            And ClassWorker.GetTilesSettings.reUseImageFiles = False Then
            ShowError("Path to PBF file is not correct.")
        ElseIf ClassWorker.GetSelection.TilesList.Count = 0 Then
            ShowError("No Tiles selected.")
        ElseIf ClassWorker.GetSelection.SpawnTile = "" Then
            ShowError("No spawn Tile selected.")
        End If
    End Sub

    Private Sub PropertyChanged(ByVal sender As Object, ByVal e As PropertyChangedEventArgs)
        Select Case e.PropertyName
            Case "LatestMessage"
                Console.WriteLine(MyGenerationWorker.LatestMessage)
                If MyGenerationWorker.LatestMessage.Contains("Finished") Then
                    Dim percentReady As Double = Math.Round((MyGenerationWorker.tilesReady / MyGenerationWorker.maxTiles) * 100, 2)
                    Console.WriteLine(percentReady & "% ready" & " - " & "Running: " & MyGenerationWorker.hoursDone & " h and " & MyGenerationWorker.minutesDone & " min" & " - " & "Time left: " & MyGenerationWorker.hoursLeft & " h and " & MyGenerationWorker.minutesLeft & " min")
                End If
            Case "GenerationComplete"
                If MyGenerationWorker.GenerationComplete = True Then
                    Console.WriteLine("Generation For '" & ClassWorker.GetWorldSettings.WorldName & "' completed")
                    If ClassWorker.GetTilesSettings.closeAfterFinish Then
                        End
                    Else
                        Console.WriteLine("Press any key to continue...")
                        Console.ReadKey()
                        End
                    End If
                End If
        End Select
    End Sub

    Sub OnClose(sender As Object, e As EventArgs)

        MyGenerationWorker.WriteLog("Console Closing")

        MyGenerationWorker.keepRunning = False
        Try
            MyGenerationWorker.cts.Cancel()
        Catch ex As Exception
            MyGenerationWorker.WriteLog(ex.Message)
        End Try
        For Each SingleProcess In MyGenerationWorker.processList
            SingleProcess.Kill()
        Next
        Try
            If Not MyGenerationWorker.neuerthread Is Nothing Then
                MyGenerationWorker.neuerthread.Abort()
            End If
        Catch ex As Exception
            MyGenerationWorker.WriteLog(ex.Message)
        End Try

        If ClassWorker.GetTilesSettings.processKilling Then
            For Each singleProcess In Process.GetProcesses()
                If singleProcess.ProcessName = "wget" Or singleProcess.ProcessName = "osmfilter" Or singleProcess.ProcessName = "osmconvert" Or singleProcess.ProcessName = "qgis-bin" Or singleProcess.ProcessName = "wpscript" Then
                    Try
                        MyGenerationWorker.WriteLog($"{singleProcess.ProcessName}.exe killed")
                        singleProcess.Kill()
                    Catch ex As Exception
                        MyGenerationWorker.WriteLog(ex.Message)
                    End Try
                End If
            Next
        End If

    End Sub

End Module
