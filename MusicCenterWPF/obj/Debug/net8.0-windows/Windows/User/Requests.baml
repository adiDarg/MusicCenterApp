   M S B A M L   `   `   `  ����I  EMusicCenterWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null?$clr-namespace:MusicCenterWPF.WindowsMusicCenterWPF.Windows  5   	   M+clr-namespace:MusicCenterWPF.Windows.SharedMusicCenterWPF.Windows.Shared  5   	   %    MusicCenterWPF.Windows.Requests   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   6local$clr-namespace:MusicCenterWPF.Windows     5   	   >shared+clr-namespace:MusicCenterWPF.Windows.Shared     5   	     � Title$  Requests��5	   	   $	��450��6   $	��800��6'    � WindowStyle$
 None=�5
   	    � WindowState$ 	Maximized=�5   	    � 
ResizeMode$ CanMinimize=�5   	    � AllowsTransparency$ False��5   	   .��5      �  � RowDefinitions 5   
   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      $
��0.3* �6   �� 5      .+�5   
   4   .MusicCenterWPF.Windows.Shared.CollapseAndClose  *   $MusicCenterWPF.Windows.Shared.NavBar  5   
   $��1��6   �� 5   
      StyledLabel#��[  6   $��2��65   .��6B   View Requests6Q   �� 5   
   $
��Auto=�6   $��3��6:   .��5      �  � ColumnDefinitions 5      �� 5      �� 5      .+�5      � -   5        $��RequestsSentGrid��5      $��0��6/    5      �� 5       $
��0.2* �6(   �� 5!      .+�5#      �� #��[  6   .��6A   Requests Sent6P   �� 5$      -   5        $��RequestsSent��5$   !   $��1��65   5&      5'      � 5(      -   5        $��RequestsReceivedGrid��5(      $��1��63    5)      �� 5*      $
��0.2* �6(   �� 5+      .+�5-      �� #��[  6   .��6A   Requests Recieved6T   �� 5.      -   5        $��RequestsReceived��5.   !   $��1��69   50      51      52      53      54      55      