﻿Imports System.Globalization
Imports System.IO
Imports System.Reflection
Imports Microsoft.Win32

''' <summary>
''' 
''' </summary>
Class MainWindow

    Dim lastChecked As String = ""

    ''' <summary>
    ''' Generates the Checkboxes on StartUp
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub ProgrammLoaded(sender As Object, e As EventArgs) Handles MyBase.Loaded
        For longitude As Integer = -89 To 90 Step 1
            For latitude As Integer = -180 To 179 Step 1
                Dim chb As New CheckBox With {
                    .IsChecked = False,
                    .Height = 4,
                    .Width = 4,
                    .Style = CType(TryFindResource("EarthTilesCheckboxStyle"), Style),
                    .HorizontalAlignment = HorizontalAlignment.Left,
                    .VerticalAlignment = VerticalAlignment.Top
                }
                AddHandler chb.Click, AddressOf Chb_CheckedChanged
                Dim TilesMargin As Thickness
                TilesMargin.Left = (latitude * 4) + 720
                TilesMargin.Top = (longitude * 4) + 356
                chb.Margin = TilesMargin

                Dim latitudeDirection As String = ""
                If (latitude < 0) Then
                    latitudeDirection = "W"
                Else
                    latitudeDirection = "E"
                End If

                Dim longitudeDirection As String = ""
                If (longitude < 1) Then
                    longitudeDirection = "N"
                Else
                    longitudeDirection = "S"
                End If

                Dim latitudeTemp As Integer = latitude
                If (latitudeTemp < 0) Then
                    latitudeTemp *= -1
                End If

                Dim longitudeTemp As Integer = longitude
                If (longitudeTemp < 0) Then
                    longitudeTemp *= -1
                End If

                Dim latitudeNumber As String = ""
                If (latitudeTemp < 10) Then
                    latitudeNumber = "00" & latitudeTemp.ToString
                ElseIf (latitudeTemp < 100) Then
                    latitudeNumber = "0" & latitudeTemp.ToString
                Else
                    latitudeNumber = latitudeTemp.ToString
                End If

                Dim longitudeNumber As String = ""
                If (longitudeTemp < 10) Then
                    longitudeNumber = "0" & longitudeTemp.ToString
                Else
                    longitudeNumber = longitudeTemp.ToString
                End If

                chb.Name = longitudeDirection & longitudeNumber & latitudeDirection & latitudeNumber
                chb.ToolTip = longitudeDirection & longitudeNumber & latitudeDirection & latitudeNumber

                Tiles.Children.Add(chb)

                txb_PathToScriptsFolder.Text = My.Application.Info.DirectoryPath
            Next
        Next
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Chb_CheckedChanged(sender As Object, e As EventArgs)
        Dim chb As CheckBox = DirectCast(sender, CheckBox) 'Use DirectCast to cast the sender into a checkbox
        Dim currentSpawnTile As String = ""

        If My.Computer.Keyboard.ShiftKeyDown Then
            If Not lastChecked = "" Then
                Dim lastCheckedLatiDir = lastChecked.Substring(0, 1)
                Dim lastCheckedLatiNumber As Int32 = 0
                Int32.TryParse(lastChecked.Substring(1, 2), lastCheckedLatiNumber)
                Dim lastCheckedLongDir = lastChecked.Substring(3, 1)
                Dim lastCheckedLongNumber As Int32 = 0
                Int32.TryParse(lastChecked.Substring(4, 3), lastCheckedLongNumber)
                If lastCheckedLatiDir = "N" Then
                    lastCheckedLatiNumber *= -1
                End If
                If lastCheckedLongDir = "W" Then
                    lastCheckedLongNumber *= -1
                End If
                Dim currentChecked As String = chb.Name
                Dim currentCheckedLatiDir = currentChecked.Substring(0, 1)
                Dim currentCheckedLatiNumber As Int32 = 0
                Int32.TryParse(currentChecked.Substring(1, 2), currentCheckedLatiNumber)
                Dim currentCheckedLongDir = currentChecked.Substring(3, 1)
                Dim currentCheckedLongNumber As Int32 = 0
                Int32.TryParse(currentChecked.Substring(4, 3), currentCheckedLongNumber)
                If currentCheckedLatiDir = "N" Then
                    currentCheckedLatiNumber *= -1
                End If
                If currentCheckedLongDir = "W" Then
                    currentCheckedLongNumber *= -1
                End If

                Dim smallerLati As Int32 = 0
                Dim biggerLati As Int32 = 0
                Dim smallerLong As Int32 = 0
                Dim biggerLong As Int32 = 0

                If lastCheckedLatiNumber < currentCheckedLatiNumber Then
                    smallerLati = lastCheckedLatiNumber
                    biggerLati = currentCheckedLatiNumber
                Else
                    smallerLati = currentCheckedLatiNumber
                    biggerLati = lastCheckedLatiNumber
                End If

                If lastCheckedLongNumber < currentCheckedLongNumber Then
                    smallerLong = lastCheckedLongNumber
                    biggerLong = currentCheckedLongNumber
                Else
                    smallerLong = currentCheckedLongNumber
                    biggerLong = lastCheckedLongNumber
                End If

                Dim NewTilesList As List(Of String) = New List(Of String)
                For LatiFor As Int32 = smallerLati To biggerLati Step 1
                    For LongFor As Int32 = smallerLong To biggerLong Step 1
                        Dim LatiForDir As String = ""
                        Dim LatiForNum As Int32 = 0
                        If (LatiFor < 1) Then
                            LatiForDir = "N"
                            LatiForNum = LatiFor * -1
                        Else
                            LatiForDir = "S"
                            LatiForNum = LatiFor
                        End If
                        Dim LongForDir As String = ""
                        Dim LongForNum As Int32 = 0
                        If (LongFor < 0) Then
                            LongForDir = "W"
                            LongForNum = LongFor * -1
                        Else
                            LongForDir = "E"
                            LongForNum = LongFor
                        End If

                        Dim longitudeNumber As String = ""
                        If (LongForNum < 10) Then
                            longitudeNumber = "00" & LongForNum.ToString
                        ElseIf (LongForNum < 100) Then
                            longitudeNumber = "0" & LongForNum.ToString
                        Else
                            longitudeNumber = LongForNum.ToString
                        End If

                        Dim latitudeNumber As String = ""
                        If (LatiForNum < 10) Then
                            latitudeNumber = "0" & LatiForNum.ToString
                        Else
                            latitudeNumber = LatiForNum.ToString
                        End If

                        NewTilesList.Add(LatiForDir & latitudeNumber & LongForDir & longitudeNumber)
                    Next
                Next
                For Each Checkbox In Me.Tiles.Children.OfType(Of CheckBox)
                    If NewTilesList.Contains(Checkbox.Name) Then
                        Checkbox.IsChecked = True
                    Else
                        Checkbox.IsChecked = False
                    End If
                Next

            End If
        Else
            lastChecked = chb.Name
        End If

        currentSpawnTile = cbb_SpawnTile.Text
        cbb_SpawnTile.Items.Clear()

        Dim TilesList =
        (
            From T In Me.Tiles.Children.OfType(Of CheckBox)()
            Where T.IsChecked Select T.Name
        ).ToList
        For Each Tile In TilesList
            cbb_SpawnTile.Items.Add(Tile)
            If Tile = currentSpawnTile Then
                cbb_SpawnTile.Text = Tile
            End If
        Next
    End Sub

#Region "Selection"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_SaveSelection_Click(sender As Object, e As RoutedEventArgs) Handles btn_SaveSelection.Click
        Dim TilesList =
            (
                From T In Me.Tiles.Children.OfType(Of CheckBox)()
                Where T.IsChecked Select T.Name
            ).ToList
        Dim ExportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ExportPathName = My.Application.Info.DirectoryPath
        Else
            ExportPathName = txb_PathToScriptsFolder.Text
        End If
        TilesList.Sort()
        Dim selection As New Selection With {
            .TilesList = TilesList,
            .SpawnTile = cbb_SpawnTile.Text
        }
        Try
            CustomXmlSerialiser.SaveXML(ExportPathName & "\tiles.xml", selection, selection.GetType)
            MsgBox("Selection saved as 'tiles.xml'")
        Catch ex As Exception
            MsgBox("File 'tiles.xml' could not be saved")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_LoadSelection_Click(sender As Object, e As RoutedEventArgs) Handles btn_LoadSelection.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If
        Dim TilesList As List(Of String) = New List(Of String)
        If My.Computer.FileSystem.FileExists(ImportPathName & "\tiles.xml") Then
            Dim selection As New Selection
            Try
                selection = CustomXmlSerialiser.GetXMLSelection(ImportPathName & "\tiles.xml", selection, selection.GetType)
                TilesList = selection.TilesList
                cbb_SpawnTile.Items.Clear()
                For Each Tile In TilesList
                    cbb_SpawnTile.Items.Add(Tile)
                    If Tile = selection.SpawnTile Then
                        cbb_SpawnTile.Text = selection.SpawnTile
                    End If
                Next
                MsgBox("Selection loaded from 'tiles.xml'")
            Catch ex As Exception
                MsgBox("Error while parsing 'tiles.xml'. Is the file correct? " & ex.Message)
            End Try
            For Each Checkbox In Me.Tiles.Children.OfType(Of CheckBox)
                If TilesList.Contains(Checkbox.Name) Then
                    Checkbox.IsChecked = True
                Else
                    Checkbox.IsChecked = False
                End If
            Next
        Else
            MsgBox("Could not find File 'tiles.xml'")
        End If

    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_ClearSelection_Click(sender As Object, e As RoutedEventArgs) Handles btn_ClearSelection.Click
        For Each Checkbox In Me.Tiles.Children.OfType(Of CheckBox)
            Checkbox.IsChecked = False
        Next
        cbb_SpawnTile.Items.Clear()
    End Sub

#End Region

#Region "Path Selection"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_PathToScriptFolder_Click(sender As Object, e As RoutedEventArgs) Handles btn_PathToScriptsFolder.Click
        Dim MyFolderBrowserDialog As New Forms.FolderBrowserDialog With {
            .SelectedPath = My.Application.Info.DirectoryPath
        }
        If MyFolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txb_PathToScriptsFolder.Text = MyFolderBrowserDialog.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_PathToWorldPainter_Click(sender As Object, e As RoutedEventArgs) Handles btn_PathToWorldPainter.Click
        Dim WorldPainterScriptFileDialog As New OpenFileDialog With {
            .FileName = "wpscript.exe",
            .Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*",
            .FilterIndex = 1
        }
        If WorldPainterScriptFileDialog.ShowDialog() = True Then
            txb_PathToWorldPainterFile.Text = WorldPainterScriptFileDialog.FileName
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_PathToQGIS_Click(sender As Object, e As RoutedEventArgs) Handles btn_PathToQGIS.Click
        Dim MyFolderBrowserDialog As New Forms.FolderBrowserDialog With {
            .SelectedPath = My.Application.Info.DirectoryPath
        }
        If MyFolderBrowserDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            txb_PathToQGIS.Text = MyFolderBrowserDialog.SelectedPath
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_PathToMagick_Click(sender As Object, e As RoutedEventArgs) Handles btn_PathToMagick.Click
        Dim MagickFileDialog As New OpenFileDialog With {
            .FileName = "magick.exe",
            .Filter = "Exe Files (.exe)|*.exe|All Files (*.*)|*.*",
            .FilterIndex = 1
        }
        If MagickFileDialog.ShowDialog() = True Then
            txb_PathToMagick.Text = MagickFileDialog.FileName
        End If
    End Sub

#End Region

#Region "Settings"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_SaveSettings_Click(sender As Object, e As RoutedEventArgs) Handles btn_SaveSettings.Click
        Dim settings As New Settings
        settings.PathToScriptsFolder = txb_PathToScriptsFolder.Text
        settings.PathToWorldPainterFolder = txb_PathToWorldPainterFile.Text
        settings.PathToQGIS = txb_PathToQGIS.Text
        settings.PathToMagick = txb_PathToMagick.Text
        settings.BlocksPerTile = cbb_BlocksPerTile.Text
        settings.VerticalScale = cbb_verticalScale.Text
        settings.geofabrik = CBool(chb_geofabrik.IsChecked)
        settings.highways = CBool(chb_highways.IsChecked)
        settings.streets = CBool(chb_streets.IsChecked)
        settings.buildings = CBool(chb_buildings.IsChecked)
        settings.borders = CBool(chb_borders.IsChecked)
        settings.farms = CBool(chb_farms.IsChecked)
        settings.meadows = CBool(chb_meadows.IsChecked)
        settings.quarrys = CBool(chb_quarrys.IsChecked)
        settings.streams = CBool(chb_streams.IsChecked)
        settings.cubicChunks = CBool(chb_CubicChunks.IsChecked)
        Dim ExportPathName As String = My.Application.Info.DirectoryPath
        Try
            CustomXmlSerialiser.SaveXML(ExportPathName & "\settings.xml", settings, settings.GetType)
            MsgBox("Settings saved as 'settings.xml'")
        Catch ex As Exception
            MsgBox("File 'settings.xml' could not be saved")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_LoadSettings_Click(sender As Object, e As RoutedEventArgs) Handles btn_LoadSettings.Click
        Dim ImportPathName As String = My.Application.Info.DirectoryPath
        If My.Computer.FileSystem.FileExists(ImportPathName & "\settings.xml") Then
            Try
                Dim MySettings As New Settings
                MySettings = CustomXmlSerialiser.GetXMLSettings(ImportPathName & "\settings.xml", MySettings, MySettings.GetType)
                If Directory.Exists(MySettings.PathToScriptsFolder) Then
                    txb_PathToScriptsFolder.Text = MySettings.PathToScriptsFolder
                Else
                    Throw New System.Exception("Path to your script folder does not exist.")
                End If
                If File.Exists(MySettings.PathToWorldPainterFolder) Then
                    txb_PathToWorldPainterFile.Text = MySettings.PathToWorldPainterFolder
                Else
                    Throw New System.Exception("The File '" & MySettings.PathToWorldPainterFolder & "' does not exist.")
                End If
                If Directory.Exists(MySettings.PathToQGIS) Then
                    txb_PathToQGIS.Text = MySettings.PathToQGIS
                Else
                    Throw New System.Exception("The File '" & MySettings.PathToQGIS & "' does not exist.")
                End If
                If File.Exists(MySettings.PathToMagick) Then
                    txb_PathToMagick.Text = MySettings.PathToMagick
                Else
                    Throw New System.Exception("The File '" & MySettings.PathToMagick & "' does not exist.")
                End If
                If MySettings.BlocksPerTile = "512" Or MySettings.BlocksPerTile = "1024" Or MySettings.BlocksPerTile = "2048" Or MySettings.BlocksPerTile = "3072" Or MySettings.BlocksPerTile = "4096" Or MySettings.BlocksPerTile = "10240" Then
                    cbb_BlocksPerTile.SelectedValue = MySettings.BlocksPerTile
                    cbb_BlocksPerTile.Text = MySettings.BlocksPerTile
                Else
                    Throw New System.Exception("Blocks per tile '" & MySettings.BlocksPerTile & "' does not exist.")
                End If
                If MySettings.VerticalScale = "100" Or MySettings.VerticalScale = "50" Or MySettings.VerticalScale = "25" Or MySettings.VerticalScale = "10" Then
                    cbb_verticalScale.SelectedValue = MySettings.VerticalScale
                    cbb_verticalScale.Text = MySettings.VerticalScale
                Else
                    Throw New System.Exception("Vertical scale '" & MySettings.VerticalScale & "' does not exist.")
                End If

                If MySettings.geofabrik = True Then
                    chb_geofabrik.IsChecked = True
                ElseIf MySettings.geofabrik = False Then
                    chb_geofabrik.IsChecked = False
                Else
                    Throw New System.Exception("geofabrik is not defined.")
                End If

                If MySettings.highways = True Then
                    chb_highways.IsChecked = True
                ElseIf MySettings.highways = False Then
                    chb_highways.IsChecked = False
                Else
                    Throw New System.Exception("highways is not defined.")
                End If

                If MySettings.streets = True Then
                    chb_streets.IsChecked = True
                ElseIf MySettings.streets = False Then
                    chb_streets.IsChecked = False
                Else
                    Throw New System.Exception("streets is not defined.")
                End If

                If MySettings.buildings = True Then
                    chb_buildings.IsChecked = True
                ElseIf MySettings.buildings = False Then
                    chb_buildings.IsChecked = False
                Else
                    Throw New System.Exception("buildings is not defined.")
                End If

                If MySettings.borders = True Then
                    chb_borders.IsChecked = True
                ElseIf MySettings.borders = False Then
                    chb_borders.IsChecked = False
                Else
                    Throw New System.Exception("borders is not defined.")
                End If

                If MySettings.farms = True Then
                    chb_farms.IsChecked = True
                ElseIf MySettings.farms = False Then
                    chb_farms.IsChecked = False
                Else
                    Throw New System.Exception("farms is not defined.")
                End If

                If MySettings.meadows = True Then
                    chb_meadows.IsChecked = True
                ElseIf MySettings.meadows = False Then
                    chb_meadows.IsChecked = False
                Else
                    Throw New System.Exception("meadows is not defined.")
                End If

                If MySettings.quarrys = True Then
                    chb_quarrys.IsChecked = True
                ElseIf MySettings.quarrys = False Then
                    chb_quarrys.IsChecked = False
                Else
                    Throw New System.Exception("quarrys is not defined.")
                End If

                If MySettings.streams = True Then
                    chb_streams.IsChecked = True
                ElseIf MySettings.streams = False Then
                    chb_streams.IsChecked = False
                Else
                    Throw New System.Exception("streams is not defined.")
                End If

                If MySettings.cubicChunks = True Then
                    chb_CubicChunks.IsChecked = True
                ElseIf MySettings.streams = False Then
                    chb_CubicChunks.IsChecked = False
                Else
                    Throw New System.Exception("Cubic Chunks is not defined.")
                End If

                MsgBox("Settings loaded from 'settings.xml'")
            Catch ex As Exception
                MsgBox("Error while parsing 'settings.xml'. Is the file correct? " & ex.Message)
            End Try
        Else
            MsgBox("Could not find File 'settings.xml'")
        End If
    End Sub

#End Region

#Region "Export Buttons"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_osmbatExport_Click(sender As Object, e As RoutedEventArgs) Handles btn_osmbatExport.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If

        Try
            Dim TilesList =
                (
                    From T In Me.Tiles.Children.OfType(Of CheckBox)()
                    Where T.IsChecked Select T.Name
                ).ToList
            If TilesList.Count = 0 Then
                Throw New System.Exception("No Tiles selected.")
            End If
            TilesList.Sort()
            Dim OsmScriptBatchFile As System.IO.StreamWriter
            OsmScriptBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\1-osmconvert.bat", False, System.Text.Encoding.ASCII)
            For Each Tile In TilesList
                OsmScriptBatchFile.WriteLine("if not exist " & Chr(34) & ImportPathName & "\osm\" & Tile & Chr(34) & " mkdir " & Chr(34) & ImportPathName & "\osm\" & Tile & Chr(34))
            Next
            If chb_geofabrik.IsChecked = True Then
                OsmScriptBatchFile.WriteLine("osmconvert osm/download.osm.pbf -o=osm/output.o5m")

                For Each Tile In TilesList
                    Dim LatiDir = Tile.Substring(0, 1)
                    Dim LatiNumber As Int32 = 0
                    Int32.TryParse(Tile.Substring(1, 2), LatiNumber)
                    Dim LongDir = Tile.Substring(3, 1)
                    Dim LongNumber As Int32 = 0
                    Int32.TryParse(Tile.Substring(4, 3), LongNumber)
                    Dim borderW As Double = 0
                    Dim borderS As Double = 0
                    Dim borderE As Double = 0
                    Dim borderN As Double = 0
                    If LatiDir = "N" Then
                        borderS = LatiNumber - 0.5
                        borderN = LatiNumber + 1.5
                    Else
                        borderS = (-1 * LatiNumber) - 0.5
                        borderN = (-1 * LatiNumber) + 1.5
                    End If
                    If LongDir = "E" Then
                        borderW = LongNumber - 0.5
                        borderE = LongNumber + 1.5
                    Else
                        borderW = (-1 * LongNumber) - 0.5
                        borderE = (-1 * LongNumber) + 1.5
                    End If
                    OsmScriptBatchFile.WriteLine("osmconvert osm/output.o5m -b=" & borderW.ToString("###.#", New CultureInfo("en-US")) & "," & borderS.ToString("##.#", New CultureInfo("en-US")) & "," & (borderE).ToString("###.#", New CultureInfo("en-US")) & "," & (borderN).ToString("##.#", New CultureInfo("en-US")) & " -o=osm/" & Tile & "/output.osm")
                    OsmScriptBatchFile.WriteLine("SET folder=osm/" & Tile)
                    Dim OsmScript As String = ""
                    If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "/osmscript.txt") Then
                        Dim fileReader As String
                        fileReader = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "/osmscript.txt")
                        OsmScript = fileReader
                    Else
                        OsmScript = My.Resources.ResourceManager.GetString("osmscript")
                        Dim objWriter As New System.IO.StreamWriter(My.Application.Info.DirectoryPath & "/osmscript.txt")
                        objWriter.Write(OsmScript)
                        objWriter.Close()
                    End If
                    OsmScriptBatchFile.WriteLine(OsmScript)
                    OsmScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\osm\" & Tile & "\output.osm" & Chr(34) & "")
                Next
            Else
                For Each Tile In TilesList
                    Dim LatiDir = Tile.Substring(0, 1)
                    Dim LatiNumber As Int32 = 0
                    Int32.TryParse(Tile.Substring(1, 2), LatiNumber)
                    Dim LongDir = Tile.Substring(3, 1)
                    Dim LongNumber As Int32 = 0
                    Int32.TryParse(Tile.Substring(4, 3), LongNumber)
                    Dim borderW As Int32 = 0
                    Dim borderS As Int32 = 0
                    Dim borderE As Int32 = 0
                    Dim borderN As Int32 = 0
                    If LatiDir = "N" Then
                        borderS = LatiNumber
                        borderN = LatiNumber + 1
                    Else
                        borderS = -1 * LatiNumber
                        borderN = -1 * LatiNumber + 1
                    End If
                    If LongDir = "E" Then
                        borderW = LongNumber
                        borderE = LongNumber + 1
                    Else
                        borderW = -1 * LongNumber
                        borderE = -1 * LongNumber + 1
                    End If
                    OsmScriptBatchFile.WriteLine("wget -O osm/" & Tile & "/output.osm " & Chr(34) & "http://overpass-api.de/api/interpreter?data=(node(" & borderS.ToString & "," & borderW.ToString & "," & borderN.ToString & "," & borderE.ToString & ");<;>;);out;" & Chr(34) & "")
                    OsmScriptBatchFile.WriteLine("SET folder=osm/" & Tile)
                    Dim OsmScript As String = ""
                    If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "/osmscript.txt") Then
                        Dim fileReader As String
                        fileReader = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "/osmscript.txt")
                        OsmScript = fileReader
                    Else
                        OsmScript = My.Resources.ResourceManager.GetString("osmscript")
                        Dim objWriter As New System.IO.StreamWriter(My.Application.Info.DirectoryPath & "/osmscript.txt")
                        objWriter.Write(OsmScript)
                        objWriter.Close()
                    End If
                    OsmScriptBatchFile.WriteLine(OsmScript)
                    OsmScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\osm\" & Tile & "\output.osm" & Chr(34) & "")
                Next
            End If
            OsmScriptBatchFile.Close()
        Catch ex As Exception
            MsgBox("File '1-osmconvert.bat' could not be saved\n" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_QgisExport_Click(sender As Object, e As RoutedEventArgs) Handles btn_QgisExport.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If

        Try
            Dim TilesList =
            (
                From T In Me.Tiles.Children.OfType(Of CheckBox)()
                Where T.IsChecked Select T.Name
            ).ToList
            If TilesList.Count = 0 Then
                Throw New System.Exception("No Tiles selected.")
            End If
            TilesList.Sort()

            If (Not System.IO.Directory.Exists(ImportPathName & "\python\")) Then
                System.IO.Directory.CreateDirectory(ImportPathName & "\python\")
            End If

            For Each Tile In TilesList
                Dim pythonFile As System.IO.StreamWriter
                pythonFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\python\" & Tile.ToString & ".py", False, System.Text.Encoding.ASCII)
                pythonFile.WriteLine("import os" & Environment.NewLine &
                                    "from PyQt5.QtCore import *" & Environment.NewLine &
                                    Environment.NewLine &
                                    "path = '" & ImportPathName.Replace("\", "/") & "/'" & Environment.NewLine &
                                    "tile = '" & Tile & "'" & Environment.NewLine &
                                    "scale = " & cbb_BlocksPerTile.Text & Environment.NewLine &
                                    Environment.NewLine)

                Dim QgisBasescript As String = ""
                If My.Computer.FileSystem.FileExists(My.Application.Info.DirectoryPath & "/qgis.txt") Then
                    Dim fileReader As String
                    fileReader = My.Computer.FileSystem.ReadAllText(My.Application.Info.DirectoryPath & "/qgis.txt")
                    QgisBasescript = fileReader
                Else
                    QgisBasescript = My.Resources.ResourceManager.GetString("basescript")
                    Dim objWriter As New System.IO.StreamWriter(My.Application.Info.DirectoryPath & "/qgis.txt")
                    objWriter.Write(QgisBasescript)
                    objWriter.Close()
                End If
                pythonFile.WriteLine(QgisBasescript)
                pythonFile.WriteLine("os.kill(os.getpid(), 9)")
                pythonFile.Close()
            Next

            Dim ScriptBatchFile As System.IO.StreamWriter
            ScriptBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\2-qgis.bat", False, System.Text.Encoding.ASCII)
            For Each Tile In TilesList
                ScriptBatchFile.WriteLine("if not exist " & Chr(34) & ImportPathName & "\image_exports\" & Tile & Chr(34) & " mkdir " & Chr(34) & ImportPathName & "\image_exports\" & Tile & Chr(34))
                ScriptBatchFile.WriteLine("if not exist " & Chr(34) & ImportPathName & "\image_exports\" & Tile & "\heightmap" & Chr(34) & " mkdir " & Chr(34) & ImportPathName & "\image_exports\" & Tile & "\heightmap" & Chr(34))
                ScriptBatchFile.WriteLine("xcopy " & Chr(34) & ImportPathName & "\osm\" & Tile & Chr(34) & " " & Chr(34) & ImportPathName & "\QGIS\OsmData" & Chr(34) & " /Y")
                ScriptBatchFile.WriteLine(Chr(34) & txb_PathToQGIS.Text & "\bin\qgis-bin.exe" & Chr(34) & " --project " & Chr(34) & ImportPathName & "\QGIS\MinecraftEarthTiles.qgz" & Chr(34) & " --code " & Chr(34) & ImportPathName & "\python\" & Tile & ".py" & Chr(34))
            Next
            ScriptBatchFile.Close()
        Catch ex As Exception
            MsgBox("File '2-qgis.bat' could not be saved\n" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_tartool_Click(sender As Object, e As RoutedEventArgs) Handles btn_tartool.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If
        Try
            Dim TilesList =
                (
                    From T In Me.Tiles.Children.OfType(Of CheckBox)()
                    Where T.IsChecked Select T.Name
                ).ToList
            If TilesList.Count = 0 Then
                Throw New System.Exception("No Tiles selected.")
            End If
            TilesList.Sort()
            Dim GdalBatchFile As System.IO.StreamWriter
            GdalBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\3-tartool.bat", False, System.Text.Encoding.ASCII)
            For Each Tile In TilesList
                Dim LatiDir = Tile.Substring(0, 1)
                Dim LatiNumber As Int32 = 0
                Int32.TryParse(Tile.Substring(1, 2), LatiNumber)
                Dim LongDir = Tile.Substring(3, 1)
                Dim LongNumber As Int32 = 0
                Int32.TryParse(Tile.Substring(4, 3), LongNumber)
                Dim TilesOneMoreDigit = LatiDir
                If LatiNumber < 10 Then
                    TilesOneMoreDigit = TilesOneMoreDigit & "00" & LatiNumber.ToString
                Else
                    TilesOneMoreDigit = TilesOneMoreDigit & "0" & LatiNumber.ToString
                End If
                TilesOneMoreDigit &= LongDir
                If LongNumber < 10 Then
                    TilesOneMoreDigit = TilesOneMoreDigit & "00" & LongNumber
                ElseIf LongNumber < 100 Then
                    TilesOneMoreDigit = TilesOneMoreDigit & "0" & LongNumber
                Else
                    TilesOneMoreDigit &= LongNumber
                End If
                Dim TilesRounded As String = TilesOneMoreDigit
                If LatiDir = "N" Then
                    Dim LatiRounded As Int32 = 0
                    Int32.TryParse(TilesOneMoreDigit.Substring(3, 1), LatiRounded)
                    If LatiRounded = 6 Or LatiRounded = 7 Or LatiRounded = 8 Or LatiRounded = 9 Then
                        TilesRounded = TilesRounded.Remove(3, 1)
                        TilesRounded = TilesRounded.Insert(3, "5")
                    ElseIf LatiRounded = 1 Or LatiRounded = 2 Or LatiRounded = 3 Or LatiRounded = 4 Then
                        TilesRounded = TilesRounded.Remove(3, 1)
                        TilesRounded = TilesRounded.Insert(3, "0")
                    End If
                Else
                    Dim LatiRounded1 As Int32 = 0
                    Int32.TryParse(TilesOneMoreDigit.Substring(3, 1), LatiRounded1)
                    Dim LatiRounded10 As Int32 = 0
                    Int32.TryParse(TilesOneMoreDigit.Substring(2, 1), LatiRounded10)
                    If LatiRounded1 = 6 Or LatiRounded1 = 7 Or LatiRounded1 = 8 Or LatiRounded1 = 9 Then
                        TilesRounded = TilesRounded.Remove(3, 1)
                        TilesRounded = TilesRounded.Insert(3, "0")
                        TilesRounded = TilesRounded.Remove(2, 1)
                        TilesRounded = TilesRounded.Insert(2, (LatiRounded10 + 1).ToString)
                    ElseIf LatiRounded1 = 1 Or LatiRounded1 = 2 Or LatiRounded1 = 3 Or LatiRounded1 = 4 Then
                        TilesRounded = TilesRounded.Remove(3, 1)
                        TilesRounded = TilesRounded.Insert(3, "5")
                    End If
                End If
                If LongDir = "E" Then
                    Dim LongRounded As Int32 = 0
                    Int32.TryParse(TilesOneMoreDigit.Substring(7, 1), LongRounded)
                    If LongRounded = 5 Or LongRounded = 6 Or LongRounded = 7 Or LongRounded = 8 Or LongRounded = 9 Then
                        TilesRounded = TilesRounded.Remove(7, 1)
                        TilesRounded = TilesRounded.Insert(7, "5")
                    Else
                        TilesRounded = TilesRounded.Remove(7, 1)
                        TilesRounded = TilesRounded.Insert(7, "0")
                    End If
                Else
                    Dim LongRounded1 As Int32 = 0
                    Int32.TryParse(TilesOneMoreDigit.Substring(7, 1), LongRounded1)
                    Dim LongRounded10 As Int32 = 0
                    Int32.TryParse(TilesOneMoreDigit.Substring(6, 1), LongRounded10)
                    If LongRounded10 = 9 And LongRounded1 >= 6 Then
                        TilesRounded = TilesRounded.Remove(7, 1)
                        TilesRounded = TilesRounded.Insert(7, "0")
                        TilesRounded = TilesRounded.Remove(6, 1)
                        TilesRounded = TilesRounded.Insert(6, "0")
                        TilesRounded = TilesRounded.Remove(5, 1)
                        TilesRounded = TilesRounded.Insert(5, "1")
                    ElseIf LongRounded1 = 6 Or LongRounded1 = 7 Or LongRounded1 = 8 Or LongRounded1 = 9 Then
                        TilesRounded = TilesRounded.Remove(7, 1)
                        TilesRounded = TilesRounded.Insert(7, "0")
                        TilesRounded = TilesRounded.Remove(6, 1)
                        TilesRounded = TilesRounded.Insert(6, (LongRounded10 + 1).ToString)
                    ElseIf LongRounded1 = 1 Or LongRounded1 = 2 Or LongRounded1 = 3 Or LongRounded1 = 4 Then
                        TilesRounded = TilesRounded.Remove(7, 1)
                        TilesRounded = TilesRounded.Insert(7, "5")
                    End If
                End If
                GdalBatchFile.WriteLine("wget -O image_exports/" & Tile & "/" & Tile & ".tar.gz " & Chr(34) & "ftp://ftp.eorc.jaxa.jp/pub/ALOS/ext1/AW3D30/release_v1903/" & TilesRounded & "/" & TilesOneMoreDigit & ".tar.gz" & Chr(34))
                GdalBatchFile.WriteLine("TarTool image_exports/" & Tile & "/" & Tile & ".tar.gz image_exports/" & Tile & "/heightmap")
            Next
            GdalBatchFile.Close()
        Catch ex As Exception
            MsgBox("File '3-tartool.bat' could not be saved\n" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_gdalExport_Click(sender As Object, e As RoutedEventArgs) Handles btn_gdalExport.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If
        If txb_PathToQGIS.Text = "" Then
            MsgBox("Please enter the path to the 'gdal_translator.exe' file")
        Else
            Try
                Dim TilesList =
                (
                    From T In Me.Tiles.Children.OfType(Of CheckBox)()
                    Where T.IsChecked Select T.Name
                ).ToList
                If TilesList.Count = 0 Then
                    Throw New System.Exception("No Tiles selected.")
                End If
                TilesList.Sort()
                Dim GdalBatchFile As System.IO.StreamWriter
                GdalBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\4-gdal.bat", False, System.Text.Encoding.ASCII)
                For Each Tile In TilesList
                    Dim LatiDir = Tile.Substring(0, 1)
                    Dim LatiNumber As Int32 = 0
                    Int32.TryParse(Tile.Substring(1, 2), LatiNumber)
                    Dim LongDir = Tile.Substring(3, 1)
                    Dim LongNumber As Int32 = 0
                    Int32.TryParse(Tile.Substring(4, 3), LongNumber)
                    Dim TilesOneMoreDigit = LatiDir
                    If LatiNumber < 10 Then
                        TilesOneMoreDigit = TilesOneMoreDigit & "00" & LatiNumber.ToString
                    Else
                        TilesOneMoreDigit = TilesOneMoreDigit & "0" & LatiNumber.ToString
                    End If
                    TilesOneMoreDigit &= LongDir
                    If LongNumber < 10 Then
                        TilesOneMoreDigit = TilesOneMoreDigit & "00" & LongNumber
                    ElseIf LongNumber < 100 Then
                        TilesOneMoreDigit = TilesOneMoreDigit & "0" & LongNumber
                    Else
                        TilesOneMoreDigit &= LongNumber
                    End If

                    Select Case cbb_verticalScale.Text
                        Case "100"
                            GdalBatchFile.WriteLine(Chr(34) & txb_PathToQGIS.Text & "\bin\gdal_translate.exe" & Chr(34) & " -a_nodata none -outsize " & cbb_BlocksPerTile.Text & " " & cbb_BlocksPerTile.Text & " -Of PNG -ot UInt16 -scale -6200 6547300 0 65535 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & TilesOneMoreDigit & "\" & TilesOneMoreDigit & "_AVE_DSM.tif" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_exported.png" & Chr(34))
                        Case "50"
                            GdalBatchFile.WriteLine(Chr(34) & txb_PathToQGIS.Text & "\bin\gdal_translate.exe" & Chr(34) & " -a_nodata none -outsize " & cbb_BlocksPerTile.Text & " " & cbb_BlocksPerTile.Text & " -Of PNG -ot UInt16 -scale -3100 3273650 0 65535 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & TilesOneMoreDigit & "\" & TilesOneMoreDigit & "_AVE_DSM.tif" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_exported.png" & Chr(34))
                        Case "25"
                            GdalBatchFile.WriteLine(Chr(34) & txb_PathToQGIS.Text & "\bin\gdal_translate.exe" & Chr(34) & " -a_nodata none -outsize " & cbb_BlocksPerTile.Text & " " & cbb_BlocksPerTile.Text & " -Of PNG -ot UInt16 -scale -1550 1636825 0 65535 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & TilesOneMoreDigit & "\" & TilesOneMoreDigit & "_AVE_DSM.tif" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_exported.png" & Chr(34))
                        Case "10"
                            GdalBatchFile.WriteLine(Chr(34) & txb_PathToQGIS.Text & "\bin\gdal_translate.exe" & Chr(34) & " -a_nodata none -outsize " & cbb_BlocksPerTile.Text & " " & cbb_BlocksPerTile.Text & " -Of PNG -ot UInt16 -scale -620 654730‬ 0 65535 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & TilesOneMoreDigit & "\" & TilesOneMoreDigit & "_AVE_DSM.tif" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_exported.png" & Chr(34))
                    End Select
                    'GdalBatchFile.WriteLine(Chr(34) & txb_PathToQGIS.Text & "\bin\gdal_translate.exe" & Chr(34) & " -a_nodata none -outsize " & cbb_BlocksPerTile.Text & " " & cbb_BlocksPerTile.Text & " -Of PNG -ot UInt16 -scale -512 15872‬ 0 65535 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & TilesOneMoreDigit & "\" & TilesOneMoreDigit & "_AVE_DSM.tif" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_exported.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToQGIS.Text & Chr(34) & "\bin\gdaldem.exe" & " slope " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & TilesOneMoreDigit & "\" & TilesOneMoreDigit & "_AVE_DSM.tif" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & TilesOneMoreDigit & "\" & TilesOneMoreDigit & "_AVE_DSM_slope.tif" & Chr(34) & " -s 111120 -compute_edges")
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToQGIS.Text & "\bin\gdal_translate.exe" & Chr(34) & " -a_nodata none -outsize " & cbb_BlocksPerTile.Text & " " & cbb_BlocksPerTile.Text & " -Of PNG -ot UInt16 -scale 0 90 0 65535 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & TilesOneMoreDigit & "\" & TilesOneMoreDigit & "_AVE_DSM_slope.tif" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_slope.png" & Chr(34))
                Next
                GdalBatchFile.Close()
            Catch ex As Exception
                MsgBox("File '4-gdal.bat' could not be saved\n" & ex.Message)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_magick_Export_Click(sender As Object, e As RoutedEventArgs) Handles btn_Magick_Export.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If
        If txb_PathToMagick.Text = "" Then
            MsgBox("Please enter the path to the 'magick.exe' file")
        Else
            Try
                Dim TilesList =
                (
                    From T In Me.Tiles.Children.OfType(Of CheckBox)()
                    Where T.IsChecked Select T.Name
                ).ToList
                If TilesList.Count = 0 Then
                    Throw New System.Exception("No Tiles selected.")
                End If
                TilesList.Sort()
                Dim GdalBatchFile As System.IO.StreamWriter
                GdalBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\5-magick.bat", False, System.Text.Encoding.ASCII)

                Dim NumberOfResize = "( +clone -resize 50%% )"
                Dim NumberOfResizeWater = "( +clone -filter Gaussian -resize 50%% -morphology Dilate Gaussian )"
                Dim TilesSize As Double = CType(cbb_BlocksPerTile.Text, Double)
                While TilesSize >= 2
                    NumberOfResize &= " ( +clone -resize 50%% )"
                    NumberOfResizeWater &= " ( +clone -filter Gaussian -resize 50%% -morphology Dilate Gaussian )"
                    TilesSize /= 2
                End While
                For Each Tile In TilesList
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_terrain.png" & Chr(34) & " -dither None -remap " & Chr(34) & txb_PathToScriptsFolder.Text & "\QGIS\terrain\" & cbb_TerrainMapping.Text & ".png" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_terrain_reduced_colors.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_exported.png" & Chr(34) & " -transparent black -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_removed_invalid.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_removed_invalid.png" & Chr(34) & " -channel A -morphology EdgeIn Diamond -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_edges.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_edges.png" & Chr(34) & " " & NumberOfResize & " -layers RemoveDups -filter Gaussian -resize " & cbb_BlocksPerTile.Text & "x" & cbb_BlocksPerTile.Text & "! -reverse -background None -flatten -alpha off -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_invalid_filled.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_invalid_filled.png" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_removed_invalid.png" & Chr(34) & " -compose over -composite -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_unsmoothed.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_water.png" & Chr(34) & " -threshold 5%% -alpha off -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_water_mask.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_water_mask.png" & Chr(34) & " -transparent white -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_transparent.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_unsmoothed.png" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_transparent.png" & Chr(34) & " -compose over -composite -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_blacked.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_blacked.png" & Chr(34) & " -transparent black -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_removed.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_removed.png" & Chr(34) & " -channel A -morphology EdgeIn Diamond -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_edges.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_edges.png" & Chr(34) & " -morphology Dilate Gaussian " & NumberOfResizeWater & " -layers RemoveDups -filter Gaussian -resize " & cbb_BlocksPerTile.Text & "x" & cbb_BlocksPerTile.Text & "! -reverse -background None -flatten -alpha off -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_filled.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_filled.png" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_water_removed.png" & Chr(34) & " -compose over -composite -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & ".png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_bathymetry.png" & Chr(34) & " -transparent black -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_bathymetry_transparent.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & ".png" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_bathymetry_transparent.png" & Chr(34) & " -compose over -composite -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & ".png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\" & Tile & "_bathymetry.png" & Chr(34) & " -evaluate divide 256 -depth 16 -alpha off " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_bathymetry.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_bathymetry.png" & Chr(34) & " -transparent black -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_bathymetry_transparent.png" & Chr(34))
                    GdalBatchFile.WriteLine(Chr(34) & txb_PathToMagick.Text & Chr(34) & " convert " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & ".png" & Chr(34) & " " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & "_bathymetry_transparent.png" & Chr(34) & " -compose over -composite -depth 16 " & Chr(34) & txb_PathToScriptsFolder.Text & "\image_exports\" & Tile & "\heightmap\" & Tile & ".png" & Chr(34))
                Next
                GdalBatchFile.Close()
            Catch ex As Exception
                MsgBox("File '5-magick.bat' could not be saved\n" & ex.Message)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_WPScriptExport_Click(sender As Object, e As RoutedEventArgs) Handles btn_WPScriptExport.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If

        If txb_PathToWorldPainterFile.Text = "" Then
            MsgBox("Please enter the path to the 'wpscript.exe' file")
        Else
            Try
                Dim TilesList =
                (
                    From T In Me.Tiles.Children.OfType(Of CheckBox)()
                    Where T.IsChecked Select T.Name
                ).ToList
                If TilesList.Count = 0 Then
                    Throw New System.Exception("No Tiles selected.")
                End If
                TilesList.Sort()
                Dim ScriptBatchFile As System.IO.StreamWriter
                ScriptBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\6-wpscript.bat", False, System.Text.Encoding.ASCII)
                For Each Tile In TilesList
                    Dim LatiDir = Tile.Substring(0, 1)
                    Dim LatiNumber As Int32 = 0
                    Int32.TryParse(Tile.Substring(1, 2), LatiNumber)
                    Dim LongDir = Tile.Substring(3, 1)
                    Dim LongNumber As Int32 = 0
                    Int32.TryParse(Tile.Substring(4, 3), LongNumber)
                    Dim ReplacedString = LatiDir & " " & LatiNumber.ToString & " " & LongDir & " " & LongNumber.ToString
                    ScriptBatchFile.WriteLine(Chr(34) & txb_PathToWorldPainterFile.Text & Chr(34) & " wpscript.js " & Chr(34) & ImportPathName.Replace("\", "/") & "/" & Chr(34) & " " & ReplacedString & " " & cbb_BlocksPerTile.Text & " " & cbb_verticalScale.Text & " " & chb_highways.IsChecked.ToString & " " & chb_streets.IsChecked.ToString & " " & chb_buildings.IsChecked.ToString & " " & chb_borders.IsChecked.ToString & " " & chb_farms.IsChecked.ToString & " " & chb_meadows.IsChecked.ToString & " " & chb_quarrys.IsChecked.ToString & " " & chb_streets.IsChecked.ToString & " " & chb_CubicChunks.IsChecked.ToString)
                Next
                ScriptBatchFile.Close()
            Catch ex As Exception
                MsgBox("File '6-wpscript.bat' could not be saved\n" & ex.Message)
            End Try
        End If
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_CombineExport_Click(sender As Object, e As RoutedEventArgs) Handles btn_CombineExport.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If

        Try
            If cbb_SpawnTile.Text = "" Then
                MsgBox("Please select a SpawnTile")
            Else
                Dim ScriptBatchFile As System.IO.StreamWriter
                ScriptBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\7-combine.bat", False, System.Text.Encoding.ASCII)

                ScriptBatchFile.WriteLine("If Not exist " & Chr(34) & ImportPathName & "\world" & Chr(34) & " mkdir " & Chr(34) & ImportPathName & "\world" & Chr(34))
                ScriptBatchFile.WriteLine("rmdir /Q /S " & Chr(34) & ImportPathName & "\world" & Chr(34))
                ScriptBatchFile.WriteLine("mkdir " & Chr(34) & ImportPathName & "\world" & Chr(34))
                ScriptBatchFile.WriteLine("mkdir " & Chr(34) & ImportPathName & "\world\region" & Chr(34))
                ScriptBatchFile.WriteLine("copy " & Chr(34) & ImportPathName & "\wpscript\exports\" & cbb_SpawnTile.Text & "\level.dat" & Chr(34) & " " & Chr(34) & ImportPathName & "\world" & Chr(34))
                ScriptBatchFile.WriteLine("copy " & Chr(34) & ImportPathName & "\wpscript\exports\" & cbb_SpawnTile.Text & "\session.lock" & Chr(34) & " " & Chr(34) & ImportPathName & "\world" & Chr(34))
                ScriptBatchFile.WriteLine("pushd " & Chr(34) & ImportPathName & "\wpscript\exports\" & Chr(34))
                ScriptBatchFile.WriteLine("For /r %%i in (*.mca) do  xcopy /Y " & Chr(34) & "%%i" & Chr(34) & " " & Chr(34) & ImportPathName & "\world\region\" & Chr(34))
                ScriptBatchFile.WriteLine("popd")
                ScriptBatchFile.Close()
            End If
        Catch ex As Exception
            MsgBox("File '7-combine.bat' could not be saved\n" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_CleanUpExport_Click(sender As Object, e As RoutedEventArgs) Handles btn_CleanUpExport.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If

        Try
            Dim ScriptBatchFile As System.IO.StreamWriter
            ScriptBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\8-cleanup.bat", False, System.Text.Encoding.ASCII)
            ScriptBatchFile.WriteLine("rmdir /Q /S " & Chr(34) & ImportPathName & "\image_exports" & Chr(34))
            ScriptBatchFile.WriteLine("mkdir " & Chr(34) & ImportPathName & "\image_exports" & Chr(34))
            ScriptBatchFile.WriteLine("rmdir /Q /S " & Chr(34) & ImportPathName & "\osm" & Chr(34))
            ScriptBatchFile.WriteLine("mkdir " & Chr(34) & ImportPathName & "\osm" & Chr(34))
            ScriptBatchFile.WriteLine("rmdir /Q /S " & Chr(34) & ImportPathName & "\wpscript\backups" & Chr(34))
            ScriptBatchFile.WriteLine("mkdir " & Chr(34) & ImportPathName & "\wpscript\backups" & Chr(34))
            ScriptBatchFile.WriteLine("rmdir /Q /S " & Chr(34) & ImportPathName & "\wpscript\worldpainter_files" & Chr(34))
            ScriptBatchFile.WriteLine("mkdir " & Chr(34) & ImportPathName & "\wpscript\worldpainter_files" & Chr(34))
            ScriptBatchFile.WriteLine("rmdir /Q /S " & Chr(34) & ImportPathName & "\wpscript\exports" & Chr(34))
            ScriptBatchFile.WriteLine("mkdir " & Chr(34) & ImportPathName & "\wpscript\exports" & Chr(34))
            ScriptBatchFile.WriteLine("rmdir /Q /S " & Chr(34) & ImportPathName & "\python" & Chr(34))
            ScriptBatchFile.WriteLine("mkdir " & Chr(34) & ImportPathName & "\python" & Chr(34))
            ScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\1-osmconvert.bat" & Chr(34))
            ScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\2-qgis.bat" & Chr(34))
            ScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\3-tartool.bat" & Chr(34))
            ScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\4-gdal.bat" & Chr(34))
            ScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\5-magick.bat" & Chr(34))
            ScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\6-wpscript.bat" & Chr(34))
            ScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\7-combine.bat" & Chr(34))
            ScriptBatchFile.WriteLine("del " & Chr(34) & ImportPathName & "\8-cleanup.bat" & Chr(34))
            ScriptBatchFile.Close()
        Catch ex As Exception
            MsgBox("File '8-cleanup.bat' could not be saved\n" & ex.Message)
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btn_AllExport_Click(sender As Object, e As RoutedEventArgs) Handles btn_AllExport.Click
        Dim ImportPathName As String = ""
        If txb_PathToScriptsFolder.Text = "" Then
            ImportPathName = My.Application.Info.DirectoryPath
        Else
            ImportPathName = txb_PathToScriptsFolder.Text
        End If
        Btn_osmbatExport_Click(sender, e)
        Btn_QgisExport_Click(sender, e)
        Btn_tartool_Click(sender, e)
        Btn_gdalExport_Click(sender, e)
        Btn_magick_Export_Click(sender, e)
        Btn_WPScriptExport_Click(sender, e)
        Btn_CombineExport_Click(sender, e)
        Btn_CleanUpExport_Click(sender, e)
        Try
            Dim ScriptBatchFile As System.IO.StreamWriter
            ScriptBatchFile = My.Computer.FileSystem.OpenTextFileWriter(ImportPathName & "\0-combine.bat", False, System.Text.Encoding.ASCII)
            ScriptBatchFile.WriteLine("@echo Started %date% %time%")
            ScriptBatchFile.WriteLine("TITLE Convert OSM data")
            ScriptBatchFile.WriteLine("Call 1-osmconvert.bat")
            ScriptBatchFile.WriteLine("TITLE Export images using QGIS")
            ScriptBatchFile.WriteLine("Call 2-qgis.bat")
            ScriptBatchFile.WriteLine("TITLE Download heightmap")
            ScriptBatchFile.WriteLine("Call 3-tartool.bat")
            ScriptBatchFile.WriteLine("TITLE Convert heightmaps")
            ScriptBatchFile.WriteLine("Call 4-gdal.bat")
            ScriptBatchFile.WriteLine("TITLE Convert a lot of images")
            ScriptBatchFile.WriteLine("Call 5-magick.bat")
            ScriptBatchFile.WriteLine("TITLE Generating worlds with WorldPainter")
            ScriptBatchFile.WriteLine("Call 6-wpscript.bat")
            ScriptBatchFile.WriteLine("TITLE Combine world")
            ScriptBatchFile.WriteLine("Call 7-combine.bat")
            ScriptBatchFile.WriteLine("TITLE Finished at %date% %time%")
            ScriptBatchFile.WriteLine("@echo Completed at %date% %time%")
            ScriptBatchFile.WriteLine("PAUSE")
            ScriptBatchFile.Close()
        Catch ex As Exception
            MsgBox("File '0-combine.bat' could not be saved\n" & ex.Message)
        End Try
    End Sub

#End Region

#Region "Help Buttons"

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_osmvoncert_help_Click(sender As Object, e As RoutedEventArgs) Handles btn_osmvoncert_help.Click
        Try
            Process.Start("https://github.com/MattiBorchers/MinecraftEarthTiles/")
        Catch ex As Exception
            InputBox("Could not open website. Copy the following URL and open it with a browser:", "Error", "https://github.com/MattiBorchers/MinecraftEarthTiles/")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_qgis_help_Click(sender As Object, e As RoutedEventArgs) Handles btn_qgis_help.Click
        Try
            Process.Start("https://github.com/MattiBorchers/MinecraftEarthTiles/")
        Catch ex As Exception
            InputBox("Could not open website. Copy the following URL and open it with a browser:", "Error", "https://github.com/MattiBorchers/MinecraftEarthTiles/")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_tartool_help_Click(sender As Object, e As RoutedEventArgs) Handles btn_tartool_help.Click
        Try
            Process.Start("https://github.com/MattiBorchers/MinecraftEarthTiles/")
        Catch ex As Exception
            InputBox("Could not open website. Copy the following URL and open it with a browser:", "Error", "https://github.com/MattiBorchers/MinecraftEarthTiles/")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_gdal_help_Click(sender As Object, e As RoutedEventArgs) Handles btn_gdal_help.Click
        Try
            Process.Start("https://github.com/MattiBorchers/MinecraftEarthTiles/")
        Catch ex As Exception
            InputBox("Could not open website. Copy the following URL and open it with a browser:", "Error", "https://github.com/MattiBorchers/MinecraftEarthTiles/")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_wpscript_help_Click(sender As Object, e As RoutedEventArgs) Handles btn_wpscript_help.Click
        Try
            Process.Start("https://github.com/MattiBorchers/MinecraftEarthTiles/")
        Catch ex As Exception
            InputBox("Could not open website. Copy the following URL and open it with a browser:", "Error", "https://github.com/MattiBorchers/MinecraftEarthTiles/")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btn_magick_help_Click(sender As Object, e As RoutedEventArgs) Handles btn_magick_help.Click
        Try
            Process.Start("https://github.com/MattiBorchers/MinecraftEarthTiles/")
        Catch ex As Exception
            InputBox("Could not open website. Copy the following URL and open it with a browser:", "Error", "https://github.com/MattiBorchers/MinecraftEarthTiles/")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub btn_Combine_help_Click(sender As Object, e As RoutedEventArgs) Handles btn_Combine_help.Click
        Try
            Process.Start("https://github.com/MattiBorchers/MinecraftEarthTiles/")
        Catch ex As Exception
            InputBox("Could not open website. Copy the following URL and open it with a browser:", "Error", "https://github.com/MattiBorchers/MinecraftEarthTiles/")
        End Try
    End Sub

    ''' <summary>
    ''' 
    ''' </summary>
    ''' <param name="sender"></param>
    ''' <param name="e"></param>
    Private Sub Btn_CleanUp_help_Click(sender As Object, e As RoutedEventArgs) Handles btn_CleanUp_help.Click
        Try
            Process.Start("https://github.com/MattiBorchers/MinecraftEarthTiles/")
        Catch ex As Exception
            InputBox("Could not open website. Copy the following URL and open it with a browser:", "Error", "https://github.com/MattiBorchers/MinecraftEarthTiles/")
        End Try
    End Sub

#End Region

End Class
