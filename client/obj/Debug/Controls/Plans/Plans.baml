   M S B A M L   `   `   `  ����A  =client, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullclr-namespace:clientclient  5          client.Plans   5      R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:client     5      X TXceed.Wpf.Toolkit, Version=4.0.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f44xctk)http://schemas.xceed.com/wpf/xaml/toolkit  5      .��5
      � ��6   Z� .+�5   
   �� .+�5      �� $	��Top=�5      $��10,10,10,20q�5      .+�5      ��   �� Orientation$  
Horizontal=�6   $��0,0,0,10q�66   .+�5      �� $��Дата и время:��5      $
��Left=�5      $	��Top=�5      
�������$��Verdana)�5   ,   $��16(�6A   &   Xceed.Wpf.Toolkit.DateTimePicker  5      -   5          Name$ 
DatePicker��5      $	��200��5      $��30��5      ���	#FF232323���	#FF424242
��������  �Xceed.Wpf.Toolkit.Primitives.DateTimeUpDownBase`1[[System.Nullable`1[[System.DateTime, mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]], mscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089]]   CurrentDateTimePart$ Hour24=�5      &   Xceed.Wpf.Toolkit.DateTimeUpDown   Format$ Custom=�5         FormatString$ dd.MM.yyyy HH:mm��5          
TimeFormat$ Custom=�5!         TimeFormatString$ dd.MM.yyyy HH:mm��5"         Kind$ Local=�5#      5%      �� 5&      $  
Horizontal=�6   $��0,0,0,10q�66   .+�5'      �� -   5         ��Name$ ButtonChooseFile��5(      $��Выбрать файл��5)      $
��Left=�5*      $	��Top=�5+      $	��150��5,      $��40��5-      $��0,0,5,0q�5.      ���	#FF232323
�������$��Verdana)�52      $��16(�53      	 � Cursor$
	 Hand��54      ��5/      Z� 54   '   �� 56      -   5        $ ButtonChooseDir��57      $)��#Выбрать директорию��58      $
��Left=�59      $	��Top=�5:      $	��200��5;      $��40��5<      $��0,0,5,0q�5=      ���	#FF232323
�������$��Verdana)�5A      $��16(�5B      $
	 Hand��5C      ��5>      Z� 5C   '   �� 5E      $��	Путь:��5F      $
��Left=�5G      $��Center=�5H      
�������$��Verdana)�5J      $��16(�5K      �� 5M      -   5        
 ��Name$
 	LabelPath��5N      $	��...��5O      $
��Left=�5P      $��Center=�5Q      
�������$��Verdana)�5S      $��12(�5T      5V      �� 5W      -   5        $ ButtonAddDate��5X      $0��*Добавить план проверки��5Y      $
��Left=�5Z      $	��Top=�5[      $	��250��5\      $��40��5]      ���	#FF4DD0A9
�������$��Verdana)�5a      $��16(�5b      $��5q�5c      $
	 Hand��5d      ��5^      Z� 5d   #   5f      �� 5g      $6��0Запланированные проверки:��5h      $
��Left=�5i      $��10,0,0,0q�5j      $	��Top=�5k      
�������$��Verdana)�5m      $��16(�5n      $��5,15,5,5q�5o      �� 5q      ���	#FFEEEEEE �� CornerRadius$ 5��5s      $	��0.5q�5t      $��10q�5u      ._�5w      �� ��. .��5x      �� -   5         ��Name$ 
StackPanel��5y      $	��150��5z       �� CanVerticallyScroll . ��. 5|      5~      5      5�      5�      5�      