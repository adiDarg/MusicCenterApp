   M S B A M L   `   `   `  ����I  EMusicCenterWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MusicCenterWPFMusicCenterWPF  5      b ^PresentationFramework.Aero2, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35hKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2Microsoft.Windows.Themes 5          Mu.App   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      .localclr-namespace:MusicCenterWPF     5      ]themeKclr-namespace:Microsoft.Windows.Themes;assembly=PresentationFramework.Aero2     5        �� 	Resources  5       �� MergedDictionaries �� Source   ComboBox.Static.Background �� EndPoint �� 
StartPoint � Color � Offset  ComboBox.Static.Border �� Color  ComboBox.Static.Glyph ' #ComboBox.Static.Editable.Background # ComboBox.Static.Editable.Border . *ComboBox.Static.Editable.Button.Background * &ComboBox.Static.Editable.Button.Border ! ComboBox.MouseOver.Background  ComboBox.MouseOver.Border 	 ComboBox.MouseOver.Glyph *
 &ComboBox.MouseOver.Editable.Background & "ComboBox.MouseOver.Editable.Border 1 -ComboBox.MouseOver.Editable.Button.Background - )ComboBox.MouseOver.Editable.Button.Border  ComboBox.Pressed.Background  ComboBox.Pressed.Border  ComboBox.Pressed.Glyph ( $ComboBox.Pressed.Editable.Background $  ComboBox.Pressed.Editable.Border / +ComboBox.Pressed.Editable.Button.Background + 'ComboBox.Pressed.Editable.Button.Border   ComboBox.Disabled.Background  ComboBox.Disabled.Border  ComboBox.Disabled.Glyph ) %ComboBox.Disabled.Editable.Background % !ComboBox.Disabled.Editable.Border 0 ,ComboBox.Disabled.Editable.Button.Background , (ComboBox.Disabled.Editable.Button.Border  ComboBoxToggleButton �� 
TargetType	 �� Property
 �� Value �� 
TargetType ��Name J� SnapsToDevicePixels F�Name �� Triggers ^� 
Conditions �� Binding �� RelativeSource �� AncestorType �� Value �� Mode �� 
TargetName P� Property P� Value  ComboBoxTemplate2 �Name � ColumnDefinitions �Name � AllowsTransparency �� Mode5  /Microsoft.Windows.Themes.SystemDropShadowChrome  Name   Color  �� MaxDropDownHeight! �� ElementName" ��Name# �� ClearTypeHint$ ��Name% ��Name& ��Name-  'System.Windows.Input.KeyboardNavigation'   DirectionalNavigation( d�Name) d� 	IsChecked* ��Name#+ �� SelectionBoxItemStringFormat, �� ContentStringFormat- �� SelectionBoxItemTemplate. �� SelectionBoxItem/ J� IsHitTestVisible0 P� 
SourceName1 ]� 
Conditions2 �� Property  ComboBoxTemplate1�� 5   
    5	      �� 5
      $# /Resources/GeneralStyles.xaml?�6%   %�'  &	        &	 �     &	 �     &	 �     &	 �     &	     &	 A    &	 f    &	     &		 %    &	
 H    &	 k    &	 �    &	 *    &	 M    &	 �    &	     &	 /    &	 R    &	 u    &	     &	 4    &	 W    &	 z    &	 �    &	 �    &	 �    &	     &	 -    7   7  7  7  7  7  7  7 	 7  7  7 
 7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  7  &	 �    7  &	 a    7  �� 5      $	 0,1&�6E   $	 0,0&�6T   .��5      � $ 	AliceBlue��6   $	 0.0Q�61   � 5      $ 	AliceBlue��6   $	 1.0Q�61   5      �� 5      $ Black��6=   �� 5      $ Black��6<   �� 5      $ 	AliceBlue��6J   �� 5      $ Black��6F   �� 5      $ Transparent��6Q   �� 5      $ Transparent��6M   �� 5      $	 0,1&�6H   $	 0,0&�6W   .��5      � $ 	#FFECF4FC��6   $	 0.0Q�61   � 5      $ 	#FFDCECFC��6   $	 1.0Q�61   5      �� 5      $ 	#FF7EB4EA��6@   �� 5      $ 	#FF000000��6?   �� 5      $ 	#FFFFFFFF��6M   �� 5      $ 	#FF7EB4EA��6I   �� 5      $	 0,1&�6X   $	 0,0&�6g   .��5      � $ 	#FFEBF4FC��6   $	 0.0Q�61   � 5       $ 	#FFDCECFC��6   $	 1.0Q�61   5!      �� 5"      $ 	#FF7EB4EA��6P   �� 5#      $	 0,1&�6F   $	 0,0&�6U   .��5$      � $ 	#FFDAECFC��6   $	 0.0Q�61   � 5%      $ 	#FFC4E0FC��6   $	 1.0Q�61   5&      �� 5'      $ 	#FF569DE5��6>   �� 5(      $ 	#FF000000��6=   �� 5)      $ 	#FFFFFFFF��6K   �� 5*      $ 	#FF569DE5��6G   �� 5+      $	 0,1&�6V   $	 0,0&�6e   .��5,      � $ 	#FFDAEBFC��6   $	 0.0Q�61   � 5-      $ 	#FFC4E0FC��6   $	 1.0Q�61   5.      �� 5/      $ 	#FF569DE5��6N   �� 50      $ 	#FFF0F0F0��6C   �� 51      $ 	#FFD9D9D9��6?   �� 52      $ 	#FFBFBFBF��6>   �� 53      $ 	#FFFFFFFF��6L   �� 54      $ 	#FFBFBFBF��6H   �� 55      $ Transparent��6S   �� 56      $ Transparent��6O   �� 57      " d�61   .�58      �� 	 �@�OverridesDefaultStyle$

 true��6:   �� 59      	 � ��$
 false��6.   �� 5:      	 � |�$
 false��6.   �� 5;      	 �@��	ClickMode$
 Press=�6.   �� 5<      	 � ��
 5=      �� 5>      " d�6*   .R�5?      �� $ templateRoot��6%   8��  8�� #��z��6�   $
 true��6�   ._�5@   "   �� $ splitBorder��6)   
������ $��1q�6X   $��Right=�6l   $��0q�6�   $
 true��6�   #��� r�6�   ._�5A   &   F� $ arrow��6+   F����        ���( ��( јaQ     јaQ P����̰( � �    P����        8�� $��Center=�6�   $��0q�6�   $��Center=�6�   5B   #   5C       5D      ^� 5E   "    5F   &   �� 5G   *    64   �� *
IsEditable+ �� " �� true6�   �� 5H   *    64   �� *IsMouseOver+ �� $
 Self=�	 false6   �� 5I   *    64   �� *	IsPressed+ �� $
 Self=�	 false6}   �� 5J   *    64   �� *	IsEnabled+ �� $
 Self=� true6}   .0�5L   &   �� $ templateRoot��6C   8
  	 � ��6]   �� 5M   &   $ templateRoot��6D   8
  	 � ��6^   �� 5N   &   $ splitBorder��6C   8
  	 � ��6\   �� 5O   &   $ splitBorder��6D   8
  	 � ��6]   5P   #   P� 5Q   "    �@J�IsMouseOver$
 true��6A   . �5R   &   �� $ arrow��6=   8
  	 � ��6P   5S   #   ^� 5T   "    5U   &   �� 5V   *    64   �� *IsMouseOver+ �� $
 Self=� true6   �� 5W   *    64   �� *
IsEditable+ �� " ��	 false6�   .0�5Y   &   �� $ templateRoot��6C   8
  	 � ��6]   �� 5Z   &   $ templateRoot��6D   8
 	 	 � ��6^   5[   #   ^� 5\   "    5]   &   �� 5^   *    64   �� *IsMouseOver+ �� $
 Self=� true6   �� 5_   *    64   �� *
IsEditable+ �� " �� true6�   .0�5a   &   �� $ templateRoot��6C   8
 
 	 � ��6]   �� 5b   &   $ templateRoot��6D   8
  	 � ��6^   �� 5c   &   $ splitBorder��6C   8
  	 � ��6\   �� 5d   &   $ splitBorder��6D   8
  	 � ��6]   5e   #   P� 5f   "    � ��$
 true��6?   . �5g   &   �� $ arrow��6=   8
  	 � ��6P   5h   #   ^� 5i   "    5j   &   �� 5k   *    64   �� *	IsPressed+ �� $
 Self=� true6}   �� 5l   *    64   �� *
IsEditable+ �� " ��	 false6�   .0�5n   &   �� $ templateRoot��6C   8
  	 � ��6]   �� 5o   &   $ templateRoot��6D   8
  	 � ��6^   5p   #   ^� 5q   "    5r   &   �� 5s   *    64   �� *	IsPressed+ �� $
 Self=� true6}   �� 5t   *    64   �� *
IsEditable+ �� " �� true6�   .0�5v   &   �� $ templateRoot��6C   8
  	 � ��6]   �� 5w   &   $ templateRoot��6D   8
  	 � ��6^   �� 5x   &   $ splitBorder��6C   8
  	 � ��6\   �� 5y   &   $ splitBorder��6D   8
  	 � ��6]   5z   #   P� 5{   "    � {�$ false��6?   . �5|   &   �� $ arrow��6=   8
  	 � ��6P   5}   #   ^� 5~   "    5   &   �� 5�   *    64   �� *	IsEnabled+ �� $
 Self=�	 false6}   �� 5�   *    64   �� *
IsEditable+ �� " ��	 false6�   .0�5�   &   �� $ templateRoot��6C   8
  	 � ��6]   �� 5�   &   $ templateRoot��6D   8
  	 � ��6^   5�   #   ^� 5�   "    5�   &   �� 5�   *    64   �� *	IsEnabled+ �� $
 Self=�	 false6}   �� 5�   *    64   �� *
IsEditable+ �� " �� true6�   .0�5�   &   �� $ templateRoot��6C   8
  	 � ��6]   �� 5�   &   $ templateRoot��6D   8
  	 � ��6^   �� 5�   &   $ splitBorder��6C   8
  	 � ��6\   �� 5�   &   $ splitBorder��6D   8
  	 � ��6]   5�   #   5�      5�      5�      �� 5�      " ��68   .R�5�      � $ templateRoot��6   $
 true��6-    5�      �� 5�      $��* �6+   �� 5�      #��� r�6+   $��0 �6~   .+�5�      � $ 
PART_Popup��6   $
 true��60   $��2��6J   $��1q�6�   $��Bottom=�6�   #��� .�6�   ��6^   �� *IsDropDownOpen+$ TwoWay=� �� $ TemplatedParent=�.��5�        $ shadow��67   $ Transparent��6G   #��z  6�   ��6[   �� *ActualWidth+$! templateRoot��._�5�      �� $ dropDownBorder��6%   #��� ��6=   #��� ��6�   $��1q�6�   ._�5�   "   �� $" DropDownScrollViewer��6/   .��5�   &   � $
 grid��6+   $# Enabled=�69   .+�5�   *   �� $$ canvas��61   $
��Left=�6A   $��0��6\   $	��Top=�6g   $��0��6   .+�5�   .   �� $% 
opaqueRect��68   ��6L   �� *
Background+$! dropDownBorder����6�   �� *ActualHeight+$! dropDownBorder����6�   �� *ActualWidth+$! dropDownBorder��5�   +   �� 5�   *   $& ItemsPresenter��69   $' 	Contained=�6Q   # z 6�   5�   '   5�   #   5�      5�      5�      d� 5�      $( toggleButton��6#   #��z��69   #��z��6c   #��z��6�   $��2��6�   8��  ) 6�   �� *IsDropDownOpen+$ TwoWay=� �� $ TemplatedParent=�6?  �� 5�      $* contentPresenter��6'   #, z+ 6A   #��z- 6�   #��z. 6�   #��z��6�   #��z��61  $/ false��6t  #��z��6�  # z 6�  #��z��6�  5�       5�      P� 5�      $0 
PART_Popup��67    �@�HasDropShadow$
 true��6O   . �5�      �� $ shadow��63   	 � ��$
 0,0,5,5q�6G   �� 5�      $ shadow��62   	 �@ Color$
 	#71000000��6F   5�      P� 5�       �@��HasItems$ false��62   . �5�      �� $ dropDownBorder��63   	 � ��$
 95��6O   5�      ]� 5�      1 5�      �� 5�      2 �@��
IsGrouping$
 true��6>   �� 5�      "2 �@%�IsVirtualizingWhenGrouping$ false��6`   ./�5�      �� 	 � ��$
 false��6J   5�      P� 5�      $0 DropDownScrollViewer��6G    � ��$ false��6i   . �5�      �� $ 
opaqueRect��67   
 6O   �� *VerticalOffset+$! DropDownScrollViewer��	 �@��Top�� 5�      $ 
opaqueRect��68   
 6P   �� *HorizontalOffset+$! DropDownScrollViewer��	 �@��Left5�      5�      �� 5�      " ��68   .R�5�      � $ templateRoot��6   $
 true��6-    5�      �� 5�      $��* �6+   �� 5�      #��� r�6+   $��0 �6~   .+�5�      � $ 
PART_Popup��6   $
 true��60   $��2��6J   $��1q�6�   $��Bottom=�6�   #��� .�6�   ��6^   �� *IsDropDownOpen+$ TwoWay=� �� $ TemplatedParent=�.��5�        $ shadow��67   $ Transparent��6G   #��z  6�   ��6[   �� *ActualWidth+$! templateRoot��._�5�      �� $ dropDownBorder��6%   #��� ��6=   #��� ��6�   $��1q�6�   ._�5�   "   �� $" DropDownScrollViewer��6/   .��5�   &   � $
 grid��6+   $# Enabled=�69   .+�5�   *   �� $$ canvas��61   $
��Left=�6A   $��0��6\   $	��Top=�6g   $��0��6   .+�5�   .   �� $% 
opaqueRect��68   ��6L   �� *
Background+$! dropDownBorder����6�   �� *ActualHeight+$! dropDownBorder����6�   �� *ActualWidth+$! dropDownBorder��5�   +   �� 5�   *   $& ItemsPresenter��69   $' 	Contained=�6Q   # z 6�   5�   '   5�   #   5�      5�      5�      d� 5�      $( toggleButton��6#   #��z��69   #��z��6c   #��z��6�   $��2��6�   8��  ) 6�   �� *IsDropDownOpen+$ TwoWay=� �� $ TemplatedParent=�6?  �� 5�      $* contentPresenter��6'   #, z+ 6A   #��z- 6�   #��z. 6�   #��z��6�   #��z��61  $/ false��6t  #��z��6�  # z 6�  #��z��6�  5�       5�      P� 5�      $0 
PART_Popup��67    �@�HasDropShadow$
 true��6O   . �5�      �� $ shadow��63   	 � ��$
 0,0,5,5q�6G   �� 5�      $ shadow��62   	 �@ Color$
 	#71000000��6F   5�      P� 5�       �@��HasItems$ false��62   . �5�      �� $ dropDownBorder��63   	 � ��$
 95��6O   5�      ]� 5�      1 5�      �� 5�      2 �@��
IsGrouping$
 true��6>   �� 5�      "2 �@%�IsVirtualizingWhenGrouping$ false��6`   ./�5�      �� 	 � ��$
 false��6J   5�      P� 5�      $0 DropDownScrollViewer��6G    � ��$ false��6i   . �5�      �� $ 
opaqueRect��67   
 6O   �� *VerticalOffset+$! DropDownScrollViewer��	 �@��Top�� 5�      $ 
opaqueRect��68   
 6P   �� *HorizontalOffset+$! DropDownScrollViewer��	 �@��Left5�      5�      5�      5�      