   M S B A M L   `   `   `  ����G  CFastComments, Version=1.3.3.0, Culture=neutral, PublicKeyToken=null+clr-namespace:FastCommentsFastComments  5   	   A%clr-namespace:FastComments.PropertiesFastComments.Properties  5   	       FastComments.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ,localclr-namespace:FastComments     5   	   3p%clr-namespace:FastComments.Properties     5   	   -   5          � Title$  FastComments��5	   	   $	��466��6   $	��673��6+    � 	Resources 5
         FlashFieldAnimation g� RepeatBehavior �� TargetProperty �� From	 �� To g� Duration g� 	BeginTime��%   &	        �� 5   
   $ 3x��61   .��5      �� $ Background.Color�6   $ WhiteX�5      $	 RedX�6   $ 0:0:0.2D�5      $ 0:0:0X�6$   �� 5      $ Background.Color�6   $	 RedX�5      $ WhiteX�6   $ 0:0:0.2D�5      $ 0:0:0.2X�6$   5      5      .��5      � .+�5   
   �� -   5         ��Name$ fullTextBox��5      $
��Left=�6'   $	��415��6B   $��
192,10,0,0q�6O   	 �� TextWrapping$
	 Wrap=�6c   $	��Top=�6w   $	��463��6�   
 �� AcceptsReturn
 . '   !FastComments.Properties.Resources   mw_fulltext_tt � ToolTip# Z 6�   6  �� 5   
      mw_code#��Z 6   $
��Left=�69   $	��Top=�6T   $��27��6l   $��50��6x   �� 5   
   -   5        $ codeTextBox��5      $
��Left=�6'   $��44��6B   $��	10,27,0,0q�6N   $
	 Wrap=�6a   $	��Top=�6u   $��75��6�   $��20(�6�   .��6�   ��5      �� 5       �� Triggers 5      �� 5       �� Binding 6'   �� *txtPassword_Flash+ �� Value True6M    N� EnterActions 5      �� 5   "   #��[  62   5       5"      .��5#      5$      �� 5%   
   -   5         ��Name$ btUndo��5%         mw_undo#��Z 6!   $
��Left=�6J   $��
100,83,0,0q�6e   $	��Top=�6y   $��75��6�   $��20��6�   {�.  6�   �� 5&   
   -   5           mw_help#��Z 5&      $
��Left=�6:   $��
10,247,0,0q�6U   $	��Top=�6i   $��75��6�   $��20��6�      
mw_help_tt# Z 6�   �� 5'   
   -   5         ��Name$ copyCheckbox��5'         mw_autocopy#��Z 6)   $
��Left=�6V   $��	10,85,0,0q�6q   $	��Top=�6�    d� 	IsChecked$
 TrueY�6�      mw_autocopy_tt# Z 6�   �� 5(   
   -   5           mw_settings#��Z 5(      $
��Left=�6>   $��
10,200,0,0q�6Y   $	��Top=�6m   $��75��6�      mw_settings_tt# Z 6�   6�   �� 5)   
   -   5        $ btEnter��5)         mw_enter#��Z 6"   $
��Left=�6M   $��
100,27,0,0q�6h   $	��Top=�6|   $��75��6�   $��44��6�    �� 	IsDefault . {�.  6�   �� 5*   
   -	   5         ��Name$ 	tbVersion��5*      $
��Left=�6'   $��
10,409,0,0q�6B   $
��Wrap=�6V   $��v. 1.0��6j   $	��Top=�6x   $��89��6�   �� 5+   
   -
   5           	mw_report#��Z 5+      $
��Left=�6<   $��
10,287,0,0q�6W   $	��Top=�6k   $��75��6�   $��20��6�   # Z 6�   �� 5,   
   -   5        $ btClear��5,          mw_clear#��Z  6"   !   mw_clear_tt# Z! 6L   $
��Left=�6�   $��100,116,0,0q�6�   $	��Top=�6�   $��75��6�   {�.  6�   5/      50      