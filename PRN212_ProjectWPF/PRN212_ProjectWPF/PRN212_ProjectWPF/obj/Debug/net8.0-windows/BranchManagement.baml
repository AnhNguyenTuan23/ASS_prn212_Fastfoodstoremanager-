   M S B A M L   `   `   `  ����L  HPRN212_ProjectWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:PRN212_ProjectWPFPRN212_ProjectWPF  5      (    "PRN212_ProjectWPF.BranchManagement   5      W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      1localclr-namespace:PRN212_ProjectWPF     5      .��5	      � 
�������.+�5
   
   � 
�������  � RowDefinitions  5      �� 5      $
��Auto �6    �� 5      $
��Auto �6    �� 5      $
��Auto �6     � ColumnDefinitions 5      �� 5      $��4* �6#   �� 5      $��2* �6#   .+�5      �� $��Center=�6   $��2��66   .+�5      �� $��30(�6   $
��Bold"�6*   Branch Management6O   5      � 5      $��1��6   $��0��6    $��10q�60   .+�5      ��  �� Orientation$ 
Horizontal=�6   $��Center=�66   .+�5      �� -   5         ��Name$ 	txtSearch��5      $	��280��62   
���   �6R   �� 5      -   5         ��Name$ 	btnSearch��5      $��Search��60   $
��Bold"�6A   $��30��6S   $��0,0,5,0q�6_   $��81��6p   5      5      &   System.Windows.Controls.DataGrid  5      -   5          Name$ dgBranchManagement��5   5   $��2��6   $��0��6$      AutoGenerateColumns$ False��6Q      CanUserAddRows$ False��6m      SelectionMode$ Single=�6�   	   Columns	 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn
   Width5  /System.Windows.Controls.DataGridLengthConverter$
 * 6)      Header$
 Name��63   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6A   �� *
BranchName+  5       $
 * 6)   $ Address��63    6D   �� *	Address+  5!      $
 * 6)   $ Phone��63    6B   �� *Phone+  5"      $

 Auto 6)   $ Status��66    6F   �� *StatusDetail+5$      �� 5%      $��2��6   $��1��6&   .+�5&      �� $
��Name��6   $
��Bold"�6(   $��15(�6:   �� 5'      -   5        $ txbName��5'      $��25��6G   ��6+   �� *
BranchName+6G   �� 5(      $��Address��6   $
��Bold"�6*   $��15(�6<   $��0,10,0,0q�6J   �� 5)      -   5        $ 
txbAddress��5)      $��25��6.   ��6:   �� *	Address+�� 5*      $��Phone��6   $
��Bold"�6(   $��15(�6:   $��0,10,0,0q�6H   �� 5+      -   5        $ txbPhone��5+      $��25��6,   ��68   �� *Phone+	� 5,      -   5         	�Name$ 	rdbActive��5,   0   $��0,10,0,0q�6    	� 	GroupName$ status��6C    d� 	IsChecked$
 trueY�6V   $��Active��6g   	� 5-      -   5        $ rdDelete��5-      $ status��60   $��Delete��6D   �� 5.      $ 
Horizontal=�6   $��0,10,0,0q�66   $	��144��6H   .+�5/      �� -	   5        $ btnAdd��5/      $	��Add��6-   $��60��6;   $
��Bold"�6F   $��0,0,20,0q�6m   �� 50      -
   5        $ 	btnUpdate��50      $��Update��60   $��60��6A   $
��Bold"�6L   $��0,0,20,0q�6_   6q   52      54      55      56      57      