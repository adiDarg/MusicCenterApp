   M S B A M L   `   `   `  ����I  EMusicCenterWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MusicCenterWPF.WindowsMusicCenterWPF.Windows  5   	   M+clr-namespace:MusicCenterWPF.Windows.SharedMusicCenterWPF.Windows.Shared  5   	   /    )MusicCenterWPF.Windows.EnterValidationKey   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:MusicCenterWPF.Windows     5   	   >shared+clr-namespace:MusicCenterWPF.Windows.Shared     5   	     � Title$  Enter Validation Key��5	   	   $	��450��6&   $	��800��63    � WindowStyle$
 None=�5
   	    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ CanMinimize=�5   	    � AllowsTransparency$ False��5   	   .��5      � $	��434��6   $��Bottom=�6    � RowDefinitions 5   
   �� 5      $
��0.5* �6   �� 5      $
��0.6* �6   �� 5      $
��0.6* �6   �� 5      �� 5      .+�5   
   4   .MusicCenterWPF.Windows.Shared.CollapseAndClose  $��0��6"   *   $MusicCenterWPF.Windows.Shared.NavBar  5   
   $��1��6   �� 5   
      StyledLabel#��[  6   $��2��65   $	��Top=�5      $
��Left=�6(   $��
309,24,0,0q�6C   .��6W   Activate Registration5      �� 5   
   -   5        $��
errorLabel��5        
ErrorLabel#��[ 6"   $��3��6F   $
��Left=�6S   $��395,10,0,144q�6n   $��2��6�   �� 5   
   -   5        $��inputTextBox��5        StyledTextBox#��[ 6&   $��Enter Validation Key��5      $��3��5      $	��223��6   �� 5    
   -   5        $��submitButton��5         StyledButton#��[ 6%   $��Submit��5!      $��4��5"      6   5#      5$      