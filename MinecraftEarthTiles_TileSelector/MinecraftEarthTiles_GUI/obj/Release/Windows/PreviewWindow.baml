   M S B A M L   `   `   `  ���� R  NMinecraftEarthTiles_GUI, Version=2.0.0.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MinecraftEarthTiles_GUIMinecraftEarthTiles_GUI  +    %MinecraftEarthTiles_GUI.PreviewWindow   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     @d2http://schemas.microsoft.com/expression/blend/2008     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  1local%clr-namespace:MinecraftEarthTiles_GUI  -     � Title$(  "Minecraft Earth Tiles Live Preview��$	��516��$	��753��$	��516��$	��753��   MaterialDesignWindow#��[   � WindowStartupLocation$ CenterScreen=�.��]� .+�o� $	��Top=� # MaterialDesignToolBarBackground#���  .@�m�  m� Icon '  !MaterialDesignThemes.Wpf.PackIcon     Kind$
 Menu=�.@�m� -   $��Close��   $ Close=�$��Center=�m� -   $
��Help��   $
 Help=�$��Center=�]� $��Bottom=� ( $MaterialDesignTextFieldBoxBackground#���  .+��� $��Right=�.+��� -    ��Name$ btn_Close_Preview��$��10q�.����  �� Orientation$ 
Horizontal=�.+�  $ Close=�$��Center=��� Close]� $��Center=�$��	10 0 10 0q�.+��� -    ��Name$ lbl_ZoomSlider��$
��Zoom��$
��Left=��� -    ��Name$ zsl_ZoomSlider�� �� Orientation$ 
Horizontal=�	 � Minimum$		 0.5Q�
 � Maximum$

 80.0Q� � LargeChange$ 1Q� � SmallChange$	 0.5Q� � Value$	 1.0Q�$��20���� -    ��Name$ ScrollViewer�� �� PanningMode$
 Both=�$��Visible=�$��Visible=�.���� $
��Left=�$	��Top=�.+�� -   $��Tiles��$
��1440��$	��720��$
��1440��$	��720��$��0,0,0,0q�$	��Top=�$
��Left=� � LayoutTransform V� .~��� -	   scaleTransform��.+��� -
    ��Name$ img_Background��$'��!../MyResources/terrain_chroma.jpg��$
��1440��$	��720��$
��1440��$	��720��$��0,0,0,0q�$	��Top=�$
��Left=� ��  �� ScaleX ��  �� ElementName$ 
ZoomSlider�� �� Path$ Value� �� ScaleY �� $ 
ZoomSlider��$ Value�