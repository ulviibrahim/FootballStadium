   M S B A M L   `   `   `  ����V  RReservationFootballStadiums, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullI)clr-namespace:ReservationFootballStadiumsReservationFootballStadiums  5   	   )    #ReservationFootballStadiums.Stadium   5   	   R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   ;local)clr-namespace:ReservationFootballStadiums     5   	   -   5          �Name$  WindowStadium��5   	    � Title$ 	Meydança��5   	   $��380.232��6   $��290.697��6+   ��5	      �� 5
   
    �� EndPoint$ 0.5,1&�6    �� 
StartPoint$ 0.5,0&�6/   .��5      �  � Color$ Black��6    � Offset$ 0Q�6)   � 5      $ 	#FFF0550C��6   $ 1Q�6-   5      .��5      � .+�5   
   �� -   5         ��Name$ LblEntry��5      $��Meydança əlavə edin��6"   $
��Left=�6@   $��	49,39,0,0q�6[   $	��Top=�6n   $	��227��6�   
�������$��20(�6�   $
��Bold"�6�   �� 5   
   -   5        $ lblAddStadium��5      $��
Meydança*��6'   $
��Left=�6;   $��
49,152,0,0q�6V   $	��Top=�6j   $	��124��6�   
�������$��14(�6�   $
��Bold"�6�   �� 5   
   -   5         ��Name$ 
TxtStadium��5      $
��Left=�6&   $��23��6A   $��
49,186,0,0q�6M    �� TextWrapping$
 Wrap=�6a   $	��Top=�6u   $	��157��6�   �� 5   
   -   5        	 ��Name$	 BtnAdd��5      $��
Əlavə et��6!   $
��Left=�64   $��
26,256,0,0q�6O   $	��Top=�6c   $	��120��6{   $��32��6�   
����������	#FF3F95DC$��16(�6�   6�   �� 5   
   -   5        
 ��Name$
 CmbStadiums��5      $
��Left=�6(   $��
49,104,0,0q�6C   $	��Top=�6W   $	��157��6o    �� DisplayMemberPath$
 Name��5       �� SelectedValuePath$ Id��5      ��5      ��  �� Path$ Stadiums� �� SelectedValue 5      �� $ Stadiums��� 5   
   -   5        $	 	BtnUpdate��5      $��Yenilə��6$   $
��Left=�65   $��
26,293,0,0q�6P   $	��Top=�6d   $	��120��6|   $��32��6�   
����������	#FF3F95DC$��16(�6�   6�   �� 5   
   -   5        $	 	BtnDelete��5      $	��Sil��6$   $
��Left=�62   $��156,256,0,0q�6M   $	��Top=�6b   $	��120��6z   $��32��6�   
����������	#FF3F95DC$��16(�6�   6�   5      5       