   M S B A M L   `   `   `  ���� R  NMinecraftEarthTiles_GUI, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MinecraftEarthTiles_GUIMinecraftEarthTiles_GUI  +    %MinecraftEarthTiles_GUI.StartupWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  1local%clr-namespace:MinecraftEarthTiles_GUI  -     � Title$  Minecraft Earth Tiles��$	��275��$	��275��$	��409��$	��409�� � 
ResizeMode$ CanMinimize=�   MaterialDesignWindow#��[   � WindowStartupLocation$ CenterScreen=�.��]� .+�o� $	��Top=� # MaterialDesignToolBarBackground#���  .@�m�  m� Icon '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Menu=�$��Center=�.@�m� -   $��btn_debugZip��$��Create Debug Zip��   $ CompressedFolder=�$��Center=��� m� -   $��Close��   $ Close=�$��Center=�m� $��Theme��   $ Brightness6=�$��Center=�.@�m� -   $��	mnu_light��$��Light�� m� IsCheckable .  m� 	IsChecked . m� -   $��mnu_dark��$
��Dark�� . m� -   $
��Help��   $
 Help=�$��Center=�m� -   $
��Info��   $ 
InfoCircle=�$��Center=��� .+��  � ColumnDefinitions �� $��1* ��� $��1* �.+��� $��0��$��10q�.+��� -    ��Name$ lbl_Setting_Status��$��Status: incomplete���� -	   	 ��Name$	 btn_Settings��.���� 
 �� Orientation$
 
Horizontal=�.+��� 
Settings  $ Settings=�$��Center=�$��5 0 0 0q��� $��1��$��10q�.+��� -
   $ lbl_Selection_Numbers��$��Tiles selected: 0���� -   $	 btn_Selection��.���� $
 
Horizontal=�.+��� 	Selection  $ SelectMultipleLocation=�$��Center=�$��5 0 0 0q��� $��10q�.+��� $
 
Horizontal=�.+��� -   $ lbl_World_Size_Label��$��Estimated world size:���� -   $ lbl_World_Size_Content��$
��0 kb���� -   $	 btn_AllExport��$��65��{�.  .���� $
 
Horizontal=�.+��� 
Generation  $ Globe=�$��Center=�$��5 0 0 0q�