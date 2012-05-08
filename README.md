Silver-QrCode-Generator
=======================

### Overview
Silver QrCode generator is a easy to use QrCode generator for Windows user. It was originally design for one of my friend, now I have published it here as Open Source software. It includes Unicode support and able to export QrCode as Image or EPS files for PDF. 

Generator's encoder is developed under QrCode.Net at [CodePlex][2] as C# based .Net library. I am one of coordinator and developer of that project, thus this project is a sub project of QrCode.Net. 
[2]: http://qrcodenet.codeplex.com/ "QrCode.Net"

### Install
Download Zip file from github and un-zip at your computer, then run "Silver QrCode generator.exe".

You can also right click that exe file and send to Desktop as shortcut. 

Generator require .Net 4.0 runtime. Which you can get from [Microsoft download page][1].
[1]: http://www.microsoft.com/en-us/download/details.aspx?id=17851 ".Net 4.0 Runtime"

### Function
URL: Encode URL within QrCode, Decoder on mobile will recognise and load it with mobile's browser.
MeCard: Docomo's specification for Phonebook registration. Mobile decoder will read and ask if you want save it to your contact list.
vCard: Business card specification. Same as Mecard, but it was originally design for other system. MeCard is more common on barcode.
Phone: Encode phone inside QrCode, Decoder can detect and ask whether you want to call that number or save it.
SMS: Similar to Phone, it will send the message you have put inside to specific phone number.
Email: Same as SMS but on email address.
Text: Anything that you would like to put inside QrCode.

All function support any language input. Some decoder internally think those specification are only for English input. Which is false. If you encounter that, please install better decoder. I would recommand i-nigma which is free and available on any mobile platform.




# License

>Silver QrCode Generator - Windows WPF application to generate QrCode.    
>Copyright (c) 2012 Canxing(Jason) He

>This program is free software: you can redistribute it and/or modify
>it under the terms of the GNU General Public License as published by
>the Free Software Foundation, either version 3 of the License, or
>(at your option) any later version.

>This program is distributed in the hope that it will be useful,
>but WITHOUT ANY WARRANTY; without even the implied warranty of
>MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
>GNU General Public License for more details.

>You should have received a copy of the GNU General Public License
>along with this program.  If not, see <http://www.gnu.org/licenses/>.