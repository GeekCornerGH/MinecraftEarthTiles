﻿'------------------------------------------------------------------------------
' <auto-generated>
'     Dieser Code wurde von einem Tool generiert.
'     Laufzeitversion:4.0.30319.42000
'
'     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
'     der Code erneut generiert wird.
' </auto-generated>
'------------------------------------------------------------------------------

Option Strict On
Option Explicit On

Imports System

Namespace My.Resources
    
    'Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    '-Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    'Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    'mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    '''<summary>
    '''  Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    '''</summary>
    <Global.System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0"),  _
     Global.System.Diagnostics.DebuggerNonUserCodeAttribute(),  _
     Global.System.Runtime.CompilerServices.CompilerGeneratedAttribute(),  _
     Global.Microsoft.VisualBasic.HideModuleNameAttribute()>  _
    Friend Module Resources
        
        Private resourceMan As Global.System.Resources.ResourceManager
        
        Private resourceCulture As Global.System.Globalization.CultureInfo
        
        '''<summary>
        '''  Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend ReadOnly Property ResourceManager() As Global.System.Resources.ResourceManager
            Get
                If Object.ReferenceEquals(resourceMan, Nothing) Then
                    Dim temp As Global.System.Resources.ResourceManager = New Global.System.Resources.ResourceManager("MinecraftEarthTiles_TileSelector.Resources", GetType(Resources).Assembly)
                    resourceMan = temp
                End If
                Return resourceMan
            End Get
        End Property
        
        '''<summary>
        '''  Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        '''  Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
        '''</summary>
        <Global.System.ComponentModel.EditorBrowsableAttribute(Global.System.ComponentModel.EditorBrowsableState.Advanced)>  _
        Friend Property Culture() As Global.System.Globalization.CultureInfo
            Get
                Return resourceCulture
            End Get
            Set
                resourceCulture = value
            End Set
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die for tile in tiles:
        '''    latDir = tile[0:1]
        '''    latNumber = float(tile[1:3])
        '''    longDir = tile[3:4]
        '''    longNumber = float(tile[4:7])
        '''
        '''    if latDir == &quot;N&quot;:
        '''        yMax = latNumber + 1
        '''    elif latDir == &quot;S&quot;:
        '''        yMax = (latNumber - 1) * -1
        '''    else:
        '''        yMax = 0
        '''
        '''    yMin = yMax -1
        '''
        '''    if longDir == &quot;E&quot;:
        '''        xMax = longNumber + 1
        '''    elif longDir == &quot;W&quot;:
        '''        xMax = (longNumber - 1) * -1
        '''    else:
        '''        xMax = 0
        '''
        '''    xMin = xMax -1
        '''    
        '''	########################### [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        '''</summary>
        Friend ReadOnly Property basescript() As String
            Get
                Return ResourceManager.GetString("basescript", resourceCulture)
            End Get
        End Property
        
        '''<summary>
        '''  Sucht eine lokalisierte Zeichenfolge, die osmfilter osm/output.o5m --verbose --keep=&quot;highway=motorway or highway=trunk&quot; -o=osm/output/highway.osm
        '''osmfilter osm/output.o5m --verbose --keep=&quot;highway=primary or highway=secondary&quot; -o=osm/output/big_road.osm
        '''
        '''osmfilter osm/output.o5m --verbose --keep=&quot;waterway=river or waterway=canal or natural=water and water=river&quot; -o=osm/output/river.osm
        '''osmfilter osm/output.o5m --verbose --keep=&quot;type=multipolygon&quot; --keep=&quot;natural=water and water=lake or natural=water and water=reservoir or natural=water or landu [Rest der Zeichenfolge wurde abgeschnitten]&quot;; ähnelt.
        '''</summary>
        Friend ReadOnly Property osmscript() As String
            Get
                Return ResourceManager.GetString("osmscript", resourceCulture)
            End Get
        End Property
    End Module
End Namespace
