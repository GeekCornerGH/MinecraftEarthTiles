   M S B A M L   `   `   `  ����   MinecraftEarthTiles_GUI.    (MinecraftEarthTiles_GUI.GenerationWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  -     � Title$&   Minecraft Earth Tiles Generation��$	��715��$	��845��$	��200��$	��845��   MaterialDesignWindow#��[   � WindowStartupLocation$ CenterScreen=� � DataContext ��  �� RelativeSource �� *Self+.��]� .+�o� $	��Top=� # MaterialDesignToolBarBackground#���  .@�m�  m� Icon '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Menu=�.@�m� -   $��btn_preview��$��Open Live Preview��{�.     $ Globe=�$��Center=�m� -   $��btn_debugZip��$��Create Debug Zip��   $ CompressedFolder=�$��Center=��� m� -   $��Close��   $ Close=�$��Center=�m� -   $��	mnu_Start��$��Specials��   $
 Play=�.@�m� -   $��btn_Generate_Batch_Files��$��Batch Files Only��   $ FileCode=�$��Center=�m� -   $��btn_Generate_OSM_Only��$��OSM Files Only��   $	 Map=�$��Center=�m� -   $��btn_Generate_Images_Only��$��Images Only��   $ 	FileImage=�$��Center=�m� -	   $$��btn_Generate_WorldPainter_Only��$��WorldPainter Files Only��   $ Globe=�$��Center=�m� -
   $��btn_Combining_Only��$��Combining Only��   $ FilePlus=�$��Center=�m� -   $��btn_Cleanup_Only��$��Cleanup Only��   $ Delete=�$��Center=�m� -   $
��Help��   $
 Help=�$��Center=�]� $��Bottom=� ( $MaterialDesignTextFieldBoxBackground#���  .+���  �� Orientation$ 
Horizontal=�$��Right=�$��10q�.+��� -    ��Name$ 	ckb_pause��$��Pause after next step��$��Right=� � ToolTip$&  Pause generation after next step��� $��10���� -   	 ��Name$	 	btn_Start��.���� $ 
Horizontal=�.+�  $
 Play=�$��Center=��� Start� $��10���� -   $	 btn_Stop��.���� $ 
Horizontal=�.+�  $
 Stop=�$��Center=��� Stop�� $ 
Horizontal=�$��Center=�$��10q�.+��� -   
 ��Name$
 lbl_elapsed_time��$��Running: 0 h and 0 min���� -   $ 
 lbl_elapsed_time_Seperator��$��/���� -   $
 lbl_Estimated_Duration��$��Time left: 0 h and 0 min��&   System.Windows.Controls.DataGrid  -     Name$ 	dgr_Tiles��   CanUserAddRows .     AutoGenerateColumns .  "   VerticalScrollBarVisibility$
 Auto=�$   HorizontalScrollBarVisibility$
 Auto=�   RowHeaderWidth$ 0Q�$��Stretch=�$��Stretch=�   Columns 4  .System.Windows.Controls.DataGridTemplateColumn  ,  &System.Windows.Controls.DataGridColumn   Header$
 Tile��   Width5  /System.Windows.Controls.DataGridLengthConverter$	 100    CellTemplate �� .R��� ����  �� Path$ TileName� �� Mode$ OneWay=� �� NotifyOnSourceUpdated .  �� UpdateSourceTrigger$ PropertyChanged=�  $ Progress��$	 200  �� .R�� 
��� � � � Minimum$ 0Q� � Maximum$	 100Q� � Value �� $ GenerationProgress�$ OneWay=� . $ PropertyChanged=�  $
 Info��$ *  �� .R��� ���� $ Comment�$ OneWay=� . $ PropertyChanged=�  $
 Open��$	 310  �� .R��� $ 
Horizontal=�.+��� -   $	��3,0q�$��Log File��
 � Tag �� $ TileName�$ OneWay=� . $ PropertyChanged=��� -   $	��3,0q�$��	OSM Files��$��5 0 0 0q� �� $ TileName�$ OneWay=� . $ PropertyChanged=��� -   $	��3,0q�$��Images��$��5 0 0 0q� �� $ TileName�$ OneWay=� . $ PropertyChanged=��� -   $	��3,0q�$��Batch Files��$��5 0 0 0q� �� $ TileName�$ OneWay=� . $ PropertyChanged=�