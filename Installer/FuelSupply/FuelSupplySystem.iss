[Setup]
AppName=FuelSupply
AppVerName=FuelSupply version 1.0.0
VersionInfoVersion=1.0.0
DefaultDirName={pf}\Ittanta
WindowVisible=false
DisableProgramGroupPage=true
OutputDir=Output
DisableDirPage =false
DefaultGroupName=Ittanta
;UsePreviousAppDir=true
UsePreviousAppDir=false
AppID={{F07136D3-9069-4397-B549-1FB3C64DD9F2}
AppPublisher=Echo Innovate IT (P) Ltd.
AppPublisherURL=http://www.ittanta.com/
AppVersion=1.0.0
UninstallDisplayIcon={app}\FuelSupply\symbol.ico
UninstallDisplayName=FuelSupply
SetupIconFile=symbol.ico
SignedUninstaller=false
CreateUninstallRegKey=true
EnableDirDoesntExistWarning=true
ShowLanguageDialog=no
OutputBaseFilename=FuelSupply Installer
WizardImageBackColor=clWhite
WizardSmallImageFile=FuelSupply.bmp
WizardImageFile=FuelSupply.bmp
MinVersion=0,5.01.2600sp3
LicenseFile=License.rtf

[Dirs]
Name: {app}\FuelSupply; Permissions: everyone-full

[Types]
Name: Primary; Description: FuelSupply Installation

[Components]
Name: FuelSupply; Description: FuelSupply; Types: Primary
Name: Database; Description: Database; Types: Primary


[Files]

Source: FuelSupply\*; DestDir: {app}\FuelSupply; Flags: restartreplace recursesubdirs createallsubdirs overwritereadonly ignoreversion; Permissions: everyone-full; Components: FuelSupply

;Icon:
Source: symbol.ico; DestDir: {app}\FuelSupply; Permissions: everyone-full; Components: FuelSupply
;Fonts:
Source: Fonts\arial.ttf; DestDir: {fonts}; Flags: onlyifdoesntexist uninsneveruninstall; FontInstall: Arial; Components: FuelSupply
;Install ReportViewer:
Source: FingerPrint\FingerTec_OFIS_Scanner_Driver_3.1_Setup.exe; DestDir: {app}\FuelSupply; Flags: deleteafterinstall; Permissions: everyone-full; Components: FuelSupply
Source: FingerPrint\ofisclient3.2setup.exe; DestDir: {app}\FuelSupply; Flags: deleteafterinstall; Permissions: everyone-full; Components: FuelSupply


;Source: WebService\*; DestDir: {app}\FuelSupply; Permissions: everyone-full; Components: WebService
;Source: DBFiles\FuelSupplyDB; DestDir: {app}\FuelSupply\dbtemp; Flags: deleteafterinstall; Permissions: everyone-full; Components: Database
;SQL Files
;Source: SQLFiles\*; DestDir: {app}\FuelSupply\dbtemp; Flags: recursesubdirs createallsubdirs replacesameversion deleteafterinstall; Permissions: everyone-full; Components: Database



[Icons]
Name: {group}\FuelSupply; Filename: {app}\FuelSupply\FuelSupply.exe; WorkingDir: {app}\FuelSupply
Name: {commondesktop}\FuelSupply; Filename: {app}\FuelSupply\FuelSupply.exe; WorkingDir: {app}\FuelSupply


[Run]
;Run Files to Install Database.
;Filename: {app}\FuelSupply\dbtemp\SQLSetup.exe; Parameters: """FuelSupply"" ""FuelSupplyINSTANCE"" ""FuelSupplyDB"" "".\\"" ""SQLEXPR\\setup.exe"" ""EchoVenta@FuelSupply"" "".\\7z.exe"""; WorkingDir: {app}\FuelSupply\dbtemp; Components: Database; StatusMsg: It may take a while to install Database. Please wait.; Flags: runhidden runminimized
;Filename: cmd.exe; Parameters: "/c rmdir /s /q ""SQLEXPR"""; WorkingDir: {app}\FuelSupply\dbtemp; Components: Database; Flags: runminimized runhidden

;Filename: {app}\FuelSupply\SQLSetup.exe; Parameters: """emenu_new"" ""FuelSupplyINSTANCE"" ""eMenuDB"" "".\\"" "".\\SQLEXPR\\setup.exe"" ""India20@20"" "".\\7z.exe"""; WorkingDir: {app}\FuelSupply; Components: Database; StatusMsg: It may take a while to install Database. Please wait.; Flags: runhidden runminimized
;Filename: cmd.exe; Parameters: "/c rmdir /s /q ""SQLEXPR"""; WorkingDir: {app}\FuelSupply; Components: Database; Flags: runminimized runhidden

FileName: {app}\FuelSupply\ofisclient3.2setup.exe; parameters: /q /norestart; Components: FuelSupply; StatusMsg: Configuring finger print driver. Please wait...; Flags: runhidden runminimized
FileName: {app}\FuelSupply\FingerTec_OFIS_Scanner_Driver_3.1_Setup.exe; parameters: /q /norestart; Components: FuelSupply; StatusMsg: Configuring finger print driver. Please wait...; Flags: runhidden runminimized
Filename: {app}\FuelSupply\rebuildIconCache.bat; StatusMsg: Refreshing windows. Please wait...;   Components: FuelSupply; Flags: runminimized runhidden

[InstallDelete]
Name: {app}\FuelSupply\commonData; Type: filesandordirs; Components: FuelSupply

[UninstallDelete]
Name: {app}\FuelSupply\commonData; Type: filesandordirs


[Code]
var
	txtAdvanceConfigPwd : TEdit;
	bCustomPageCreated : Boolean;
	txtBOIpAddress : TEdit;
	pgAdvancedConnection : TWizardPage;
	lblIpAddress : TLabel;
	ApplicationWasUninstalled: Boolean;
	strIpValue: String;
	strPortValue: String;
	installPowerShotDrivers:Boolean;
	wpDataDirPage: TInputDirWizardPage;
	chkPrivateDirectory: TCheckBox;
	strLicenseType: String;
  strDataDirectory: String;


// if  CreateAdvanceConnectionPage created and User press back button and
// Select InstllationType except FuelSupply Client, then CreateAdvanceConnectionPage should be skipped. 
function ShouldSkipPage(PageID: Integer): Boolean;
begin

  if PageID <> 100 then
  begin
     Result := False;
    exit;
  end;
  
  if WizardSetupType(true) = 'Secondary Installation' then
  begin
    Result := False;
    exit;
  end;

  Result := True;
end;
 

function InitializeSetup(): Boolean;
var
    ErrorCode: Integer;
    NetFrameWorkInstalled : Boolean;
    Result1 : Boolean;
    reportviewerRedistPath : string;
    OSName : string;
    InstalledOS : String;
    RegKeyValue : Cardinal;
    ResultCode: Integer;

begin
   Result1:=true;

	NetFrameWorkInstalled := RegKeyExists(HKLM,'SOFTWARE\Microsoft\NET Framework Setup\NDP\v4\Full');

//4.5
	if NetFrameWorkInstalled =false then
  begin
    Result1 := false;
 end;  

   if (Result1 =False) then
   begin
      MsgBox('Please install .Net Framework 4.5. Please refer installation guide for more detail.', mbCriticalError, MB_OK); 
      ShellExec('open', 'http://www.microsoft.com/en-us/download/details.aspx?id=30653', '', '', SW_SHOWNORMAL, ewNoWait, ErrorCode); 
      Result := False;
      exit;
   end;


   Result:=True;
end;


function RemoveAllFiles(DirName: string): boolean;
var
  FindRec: TFindRec;
  bResult: boolean;
begin
   if FindFirst(DirName + '\*.*', FindRec) then begin
      repeat
        if FindRec.Attributes and FILE_ATTRIBUTE_DIRECTORY = 0 then begin
          bResult:=DeleteFile(DirName + '\' + FindRec.Name);
        end;
      until not FindNext(FindRec);
   end;
   FindClose(FindRec);
end;

function DeleteDirectory(DirName: string): boolean;
var
  FindRec: TFindRec;
  bResult: boolean;
begin
   RemoveAllFiles(DirName);

   if FindFirst(DirName + '\*.*', FindRec) then begin
      repeat
        if (FindRec.Attributes and FILE_ATTRIBUTE_DIRECTORY <> 0) then begin
           if ((FindRec.Name <> '.') and (FindRec.Name <> '..')) then begin
              bResult:=DeleteDirectory(DirName + '\' + FindRec.Name);
           end;
        end;
      until not FindNext(FindRec);
   end;

   FindClose(FindRec);

   //Delete the Required Directory.
   bResult:=RemoveDir(DirName)
end;

function checkWindowsVersion() : Boolean;
var
  Version: TWindowsVersion;
begin
  GetWindowsVersionEx(Version);
  // Windows 7 version is 6.1 (workstation)
  if (Version.Major = 6)  and
     (Version.Minor = 1) and
     (Version.ProductType = VER_NT_WORKSTATION)
  then
    begin
      Result := True;
    end
  else
    begin
      Result := False;
    end;
end;



function checkWindowsVersion64() : Boolean;
var
  Version: TWindowsVersion;
begin
  GetWindowsVersionEx(Version);
  // Windows 7 version is 6.1 (workstation)
  if IsWin64
  then
    begin
		      Result := True;
    end
  else
    begin
		      Result := False;
    end;
end;




[Registry]
Root: HKLM64; Subkey: SOFTWARE\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers; ValueType: string; ValueName: {app}\FuelSupply\FuelSupply.exe; ValueData: RUNASADMIN; Flags: uninsdeletevalue; Check: checkWindowsVersion64
[Messages]
;SelectComponentsLabel2=Please Select Installation Type:
SelectComponentsLabel2=Please Select Installation Type:%n%nPrimary Installation: eMenu Server will be installed & configured on this machine. This is the default installation type. If you want eMenu Server on this machine, select this type.%n %nSecondary Installation: If you want to add additional OrderDesk in existing eMenu system, use secondary installation. It will connect to the Primary installation machine.
WizardSelectComponents=Select Installation Type
[UninstallRun]
Filename: {app}\FuelSupply\1.bat; Flags: runascurrentuser runhidden; Components:  FuelSupply;  WorkingDir: {app}\FuelSupply
