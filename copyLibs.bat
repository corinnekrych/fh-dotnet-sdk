COPY /B FHSDK\bin\Release\FHSDK.dll "Dist\lib\portable-net45+wp80+wp81+win8+wpa81\"
COPY /B FHSDK\bin\Release\FHSDK.xml "Dist\lib\portable-net45+wp80+wp81+win8+wpa81\"

COPY /B FHSDKPhone\Bin\Release\FHSDK.dll Dist\lib\wp80\
COPY /B FHSDKPhone\Bin\Release\FHSDK.xml Dist\lib\wp80\
COPY /B FHSDKPhone\Bin\Release\FHSDKPhone.dll Dist\lib\wp80\
COPY /B FHSDKPhone\Bin\Release\FHSDKPhone.xml Dist\lib\wp80\

COPY /B FHSDKPortable\bin\Release\FHSDK.dll Dist\lib\wp81\
COPY /B FHSDKPortable\bin\Release\FHSDK.xml Dist\lib\wp81\
COPY /B FHSDKPortable\Bin\Release\FHSDKPortable.dll Dist\lib\wp81\
COPY /B FHSDKPortable\Bin\Release\FHSDKPortable.xml Dist\lib\wp81\

COPY /B FHSDKPortable\bin\Release\FHSDK.dll Dist\lib\wpa81\
COPY /B FHSDKPortable\bin\Release\FHSDK.xml Dist\lib\wpa81\
COPY /B FHSDKPortable\bin\Release\FHSDKPortable.dll Dist\lib\wpa81\
COPY /B FHSDKPortable\bin\Release\FHSDKPortable.XML Dist\lib\wpa81\

COPY /B FHSDKPortable\bin\Release\FHSDKPortable.dll "Dist\lib\portable-win81+wpa81\"
COPY /B FHSDKPortable\bin\Release\FHSDKPortable.XML "Dist\lib\portable-win81+wpa81\"

COPY /B xamarin\FHXamarinAndroidSDK\bin\Release\FHSDK.dll Dist\lib\monoandroid\
COPY /B xamarin\FHXamarinAndroidSDK\bin\Release\FHSDK.xml Dist\lib\monoandroid\
COPY /B xamarin\FHXamarinAndroidSDK\bin\Release\FHXamarinAndroidSDK.dll Dist\lib\monoandroid\
COPY /B xamarin\FHXamarinAndroidSDK\bin\Release\FHXamarinAndroidSDK.xml Dist\lib\monoandroid\

REM COPY /B xamarin\FHXamarinIOSSDK\bin\Release\FHSDK.dll Dist\lib\monotouch\
REM COPY /B xamarin\FHXamarinIOSSDK\bin\Release\FHSDK.xml Dist\lib\monotouch\
REM COPY /B xamarin\FHXamarinIOSSDK\bin\Release\FHXamarinIOSSDK.dll Dist\lib\monotouch\
REM COPY /B xamarin\FHXamarinIOSSDK\bin\Release\FHXamarinIOSSDK.xml Dist\lib\monotouch\
