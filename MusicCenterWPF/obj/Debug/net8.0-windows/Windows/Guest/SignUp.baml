   M S B A M L   `   `   `  ����I  EMusicCenterWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null/clr-namespace:MusicCenterWPFMusicCenterWPF  5   	   M+clr-namespace:MusicCenterWPF.Windows.SharedMusicCenterWPF.Windows.Shared  5   	       MusicCenterWPF.SignUp   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   .localclr-namespace:MusicCenterWPF     5   	   >shared+clr-namespace:MusicCenterWPF.Windows.Shared     5   	     � Title$  Sign Up��5	   	   $	��450��6   $	��800��6&    � WindowStyle$
 None=�5
   	    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ CanMinimize=�5   	    � AllowsTransparency$ False��5   	    � 	Resources 5       �� MergedDictionaries �� Source�� 5   
    5      �� 5      $# /Resources/GeneralStyles.xaml?�6%   %    5      .��5      �� .+�5   
   4   .MusicCenterWPF.Windows.Shared.CollapseAndClose  �� 5   
      StyledLabel#��[  6   $
��30px(�5      .��5      	Sign Up5      �� 5   
   -   5        $��usernameInput��5        StyledTextBox#��[ 5      1   +MusicCenterWPF.Windows.Shared.TextBoxHelper   Placeholder$ Username��5      �� 5   
   -   5        $��passwordInput��5       #��[ 5!      $ Password��5"      �� 5#   
   -   5        $��
emailInput��5$      #��[ 5%      $ Email��5&      �� 5'   
   -   5        $��addressInput��5(      #��[ 5)      $ Address��5*      �� 5+   
   -   5        $��phoneNumberInput��5,      #��[ 5-      $ Phone Number��5.      �� 5/   
   -   5        $��
fileOpener��50        StyledButton#��[ 51      $��Select Image��52      $	��174��53      54      �� 55   
   -   5        $��SignUpButton��56      #��[ 57      $��Sign Up��58      59      5:      5;      