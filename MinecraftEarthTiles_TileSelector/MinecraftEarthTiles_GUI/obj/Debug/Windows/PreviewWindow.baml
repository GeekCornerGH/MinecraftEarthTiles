   M S B A M L   `   `   `  ����R  NMinecraftEarthTiles_GUI, Version=1.4.6.0, Culture=neutral, PublicKeyToken=nullA%clr-namespace:MinecraftEarthTiles_GUIMinecraftEarthTiles_GUI  5   	   +    %MinecraftEarthTiles_GUI.PreviewWindow   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   _ [MaterialDesignThemes.Wpf, Version=4.6.1.0, Culture=neutral, PublicKeyToken=df2a72020bd7962aFmaterialDesign1http://materialdesigninxaml.net/winfx/xaml/themes  5   	   1local%clr-namespace:MinecraftEarthTiles_GUI  5   	   -   5          � Title$(  "Minecraft Earth Tiles Live Preview��5	   	   $	��516��64   $	��753��6A   $	��516��6M   $	��753��6]      MaterialDesignWindow#��[  5
   	    � WindowStartupLocation$ CenterScreen=�5   	   .��5      ]� .+�5   
   o� $	��Top=�6    # MaterialDesignToolBarBackground#���  6$   .@�5      m�  m� Icon 5      '  !MaterialDesignThemes.Wpf.PackIcon  5         Kind$
 Menu=�6.   .@�5      m� -   5        $��Close��5       5        5      $ Close=�62   $��Center=�6?   5      5      m� 5      -   5        $
��Help��5       5        5      $
 Help=�6.   $��Center=�6:   5      5      ]� 5   
   $��Bottom=�6    ( $MaterialDesignTextFieldBoxBackground#���  6,   .+�5      �� $��Right=�6   .+�5       �� -   5         ��Name$ btn_Close_Preview��5       $��10q�6H   .��5!      ��  �� Orientation$ 
Horizontal=�6!   .+�5"        $ Close=�62   $��Center=�6?   �� 5#      Close6+   5$      5%      5&      ]� 5'      $��Center=�6   $��	10 0 10 0q�63   .+�5(      �� -   5         ��Name$ lbl_ZoomSlider��5(      $
��Zoom��60   $
��Left=�6?   �� 5)      -   5         ��Name$ zsl_ZoomSlider��5)       �� Orientation$ 
Horizontal=�61   	 � Minimum$		 0.5Q�6J   
 � Maximum$

 80.0Q�6X    � LargeChange$ 1Q�6g    � SmallChange$	 0.5Q�6w    � Value$	 1.0Q�6�   $��20��6�   5*      5+      �� 5,   
   -   5         ��Name$ ScrollViewer��5,       �� PanningMode$
 Both=�6-   $��Visible=�6@   $��Visible=�6h   .��5-      �� $
��Left=�6   $	��Top=�64   .+�5.      � -   5        $��Tiles��5.      $
��1440��6$   $	��720��61   $
��1440��6>   $	��720��6N   $��0,0,0,0q�6^   $	��Top=�6o   $
��Left=�6�    � LayoutTransform 5/      V� 50      .~�51      �� -	   5        scaleTransform��51   -   52      .+�54      �� -
   5         ��Name$ img_Background��54      $'��!../MyResources/terrain_chroma.jpg��64   $
��1440��6_   $	��720��6l   $
��1440��6y   $	��720��6�   $��0,0,0,0q�6�   $	��Top=�6�   $
��Left=�6�   55       56      �� 57       �� ScaleX 6%   ��  �� ElementName$ 
ZoomSlider�� �� Path$ Value� �� ScaleY 6[   �� $ 
ZoomSlider��$ Value�59      5:      5;      5<      