   M S B A M L   `   `   `  ���� R  NMinecraftEarthTiles_GUI, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MinecraftEarthTiles_GUIMinecraftEarthTiles_GUI  -    'MinecraftEarthTiles_GUI.SelectionWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  1local%clr-namespace:MinecraftEarthTiles_GUI  -     � Title$%  Minecraft Earth Tiles Selection��$	��516��$	��953��$	��516��$	��953��   MaterialDesignWindow#��[   � WindowStartupLocation$ CenterScreen=�.��]� .+�o� $	��Top=� # MaterialDesignToolBarBackground#���  .@�m�  m� Icon '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Menu=�.@�m� -   $��btnSave��$
��Save��   $ ContentSave=�m� -   $��btnSaveClose��$��Save and close��   $ ContentSaveMove=�m� -   $��Clear Selection��   $ Delete=�m� -   $��Load Selection from file��   $ 
FileUpload=�m� -   $��Save Selection to file��   $ FileDownload=��� m� -   $��Close��   $ Close=�m� -   $
��Help��   $
 Help=�$��Center=�]� $��Bottom=�#���  .+��� $��Right=� �� Orientation$ 
Horizontal=�$��10q�.+��� -	    ��Name$ txb_toManyTiles��
���  ��$�� ��$��Center=�� $��10���� -
    ��Name$ btn_Save_Selection��.���� $ 
Horizontal=�.+�  $ ContentSave=�$��Center=��� Save� $��10���� -   $ btn_Cancel_Selection��.���� $ 
Horizontal=�.+�  $ Close=�$��Center=��� Close�� $
��Left=�$��Center=�$ Vertical=�.+��� $ 
Horizontal=�.+��� -    ��Name$ lbl_Corners��$2��,Corner Coordinates for worldborder / chunky:���� -    ��Name$ txb_Corners��$	��180��� 	 � ColumnDefinitions	 �� $	��200 ��� $��* �.+��� $��0��$��10q�.+��� -   $ lbl_ZoomSlider��$
��Zoom���� -   
 ��Name$
 zsl_ZoomSlider�� �� Orientation$ 
Horizontal=� � Minimum$	 0.5Q� � Maximum$
 10.0Q� � LargeChange$	 0.5Q� � SmallChange$
 0.25Q� � Value$	 1.0Q��� -   $ lbl_background��$��
Background���� -    ��Name$ cbb_Background_Image��.@���  �� 
IsSelected . .��	Terrain�� .��	Borders�� -   $ lbl_Spawn_Tile��$��
Spawn Tile���� -   $ cbb_Spawn_Tile���� -   $ lbl_Void_Barrier��$��Void Barrier (in Blocks)���� -   $ cbb_Void_Barrier��.@���  . .��0�� .��512�� .��1024�� .��1536�� .��2048�� -   $ btn_Clear_Selection��$��0 10 0 0q�.���� $ 
Horizontal=�.+�  $ Delete=�$��Center=�$��5 0 0 0q��� Clear Selection� $
��25px���� $
��Wrap=�$��5q�ecClick on one corner and then Shift+Click the opposite corner to select all Tiles between that area.�� -    ��Name$ ScrollViewer��$��1�� �� PanningMode$
 Both=�$��Visible=�$��Visible=�  MaterialDesignScrollViewer#���  .���� $
��Left=�$	��Top=�.+�� -   $��Tiles��$
��1440��$	��720��$
��1440��$	��720��$��0,0,0,0q�$	��Top=�$
��Left=� � LayoutTransform V� .~��� -   scaleTransform��.+��� -    ��Name$ img_Background��$ ��../MyResources/terrain.jpg��$
��1440��$	��720��$
��1440��$	��720��$��0,0,0,0q�$	��Top=�$
��Left=�� -   $��
CheckBoxes��$
��1440��$	��720��$
��1440��$	��720��$��0,0,0,0q�$	��Top=�$
��Left=� ��  �� ScaleX ��  �� ElementName$ 
ZoomSlider�� �� Path$ Value� �� ScaleY �� $ 
ZoomSlider��$ Value�