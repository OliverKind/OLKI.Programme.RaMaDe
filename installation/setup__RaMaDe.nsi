;NSIS Installer for RaMaDe 1.0.1.2
;Using Modern Interface, Setup-Options, etc.
;Written by Oliver Kind

;--------------------------------
;Set Compressor

  SetCompress auto
  SetCompressor LZMA

;--------------------------------
;Includeings

  !include "MUI2.nsh"  ;Modern UI
  !include "Sections.nsh"
  !include "LogicLib.nsh"
  ; include a function library that includes a file/directory size reporting command
  !include "FileFunc.nsh"   ; for ${GetSize} for EstimatedSize registry entry

;--------------------------------
;Defining

  !define AppName          'RaMaDe'
  !define Version          '1.0.1.2'
  !define Company          'OLKI-Software'
  !define Comments         'Ein Programm das RAW-Dateien löscht zu denen keine Bilddatei vorhanden ist'
  !define Copyright        '2019 - Oliver Kind'
  !define FinishFile       'RaMaDe.exe'
  !define ReadmeFile       'ReadMe.Txt'
  !define EulaFile         'Eula.rtf'

;--------------------------------
;General

  ;Name and file
  Name ${AppName}
  OutFile "${AppName}__v${Version}__Setup.exe"

  ;Default installation folder
  InstallDir "$PROGRAMFILES\${Company}\${AppName}\"

  ;Get installation folder from registry if available
  InstallDirRegKey HKCU "SOFTWARE\${Company}\${AppName}" "InstallDir"

  ;Request application privileges for Windows Vista
  RequestExecutionLevel admin

  ;Interface Settings
  !define MUI_ABORTWARNING

;--------------------------------
;Pages
  ;Install Pages
  !insertmacro MUI_PAGE_WELCOME
    !define MUI_LICENSEPAGE_RADIOBUTTONS
  !insertmacro MUI_PAGE_LICENSE .\..\${EulaFile}
  !insertmacro MUI_PAGE_DIRECTORY
    !define MUI_STARTMENUPAGE_DEFAULTFOLDER "${Company} - ${AppName}"
    Var StartMenuFolder
  !insertmacro MUI_PAGE_STARTMENU  "Application" $StartMenuFolder
  !insertmacro MUI_PAGE_INSTFILES
    !define MUI_FINISHPAGE_NOAUTOCLOSE
    !define MUI_FINISHPAGE_RUN_CHECKED
    !define MUI_FINISHPAGE_RUN $INSTDIR\${FinishFile}
    !define MUI_FINISHPAGE_SHOWREADME_NOTCHECKED
    !define MUI_FINISHPAGE_SHOWREADME $INSTDIR\${ReadmeFile}
  !insertmacro MUI_PAGE_FINISH

  ;Uninstall Pages
  !insertmacro MUI_UNPAGE_WELCOME
  !insertmacro MUI_UNPAGE_CONFIRM
  !insertmacro MUI_UNPAGE_INSTFILES
     !define MUI_UNFINISHPAGE_NOAUTOCLOSE
  !insertmacro MUI_UNPAGE_FINISH

;--------------------------------
;Languages

  !insertmacro MUI_LANGUAGE "German"

;--------------------------------

;Version Information
  VIProductVersion "${Version}"
  VIAddVersionKey /LANG=${LANG_GERMAN} "ProductName" "${AppName}"
  VIAddVersionKey /LANG=${LANG_GERMAN} "Comments" "${Comments}"
  VIAddVersionKey /LANG=${LANG_GERMAN} "CompanyName" "${Company}"
  VIAddVersionKey /LANG=${LANG_GERMAN} "LegalTrademarks" "${AppName} ist ein Markenzeichen von ${Company}"
  VIAddVersionKey /LANG=${LANG_GERMAN} "LegalCopyright" "© ${Company} ${Copyright}"
  VIAddVersionKey /LANG=${LANG_GERMAN} "FileDescription" "Setup für ${AppName}"
  VIAddVersionKey /LANG=${LANG_GERMAN} "FileVersion" ${Version}

;--------------------------------

;Installer Sections
SetOverwrite on
ShowInstDetails show
ShowUnInstDetails show

;~~~~~~~~~~~~~~~~~~~~~~~

;Other Installations
Section ""

  ; Set output path to the installation directory.
  SetOutPath $INSTDIR

  File ".\..\bin\Release\Changelog.txt"
  File ".\..\bin\Release\RaMaDe.exe"
  File ".\..\bin\Release\${EulaFile}"
  File ".\..\bin\Release\${ReadmeFile}"
  File ".\..\bin\Release\Properties\Resources\RaMaDe.ico"

  SetOutPath "$INSTDIR\Licenses"
  File ".\..\bin\Release\licenses\LGPL-License.Txt"

  ;Create Desktop shortcut
  CreateShortCut "$DESKTOP\${AppName}.lnk" "$INSTDIR\RaMaDe.exe" ""

  ;Create Starmenue Entry
  !insertmacro MUI_STARTMENU_WRITE_BEGIN Application
    ;Create shortcuts
    CreateDirectory "$SMPROGRAMS\$StartMenuFolder"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\RaMaDe.lnk" "$INSTDIR\RaMaDe.exe"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Uninstall.lnk" "$INSTDIR\Uninstall.exe"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\Readme.lnk" "$INSTDIR\${ReadmeFile}"
    CreateShortCut "$SMPROGRAMS\$StartMenuFolder\EULA.lnk" "$INSTDIR\${EulaFile}"
  !insertmacro MUI_STARTMENU_WRITE_END

  ;Store installation folder for overwriting with a newer version
  WriteRegStr HKCU "SOFTWARE\${Company}\${AppName}" "InstallDir" $INSTDIR

  ;Create uninstaller
  WriteUninstaller "$INSTDIR\Uninstall.exe"
  ; write uninstall strings
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Company} ${AppName}" "DisplayName" '${AppName}'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Company} ${AppName}" "UninstallString" '$INSTDIR\Uninstall.exe'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Company} ${AppName}" "DisplayIcon" '$INSTDIR\RaMaDe.ico'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Company} ${AppName}" "DisplayVersion" '${Version}'
  WriteRegStr HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Company} ${AppName}" "Publisher" '${Company}'
    ; get cumulative size of all files in and under install dir
    ; report the total in KB (decimal)
    ; place the answer into $0  ($1 and $2 get other info we don't care about)
    ${GetSize} "$INSTDIR" "/S=0K" $0 $1 $2
    ; Convert the decimal KB value in $0 to DWORD
    ; put it right back into $0
    IntFmt $0 "0x%08X" $0
    ; Create/Write the reg key with the dword value
  WriteRegDWORD HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Company} ${AppName}" "EstimatedSize" $0
SectionEnd

;--------------------------------
;Uninstaller Section
; Uninstaller
Section "Uninstall"
  ;Delte program directory
  RMDir /R "$INSTDIR"

  ;Delte company directory if empty
  RMDir "$SMPROGRAMS\${Company}"

  ;Delete directory in AppData
  ReadRegStr $R1 HKCU "SOFTWARE\${Company}\${AppName}\" "SettingDir"
  RMDir /R $R1
  RMDir "$APPDATA\${Company}\"

  ;Delete Desktop shortcut
  Delete "$DESKTOP\${AppName}.lnk"
  ;Delete Starmenue
  !insertmacro MUI_STARTMENU_GETFOLDER Application $StartMenuFolder
  RMDir /R "$SMPROGRAMS\$StartMenuFolder"

  ;Delete registry
  DeleteRegKey HKCU "SOFTWARE\${Company}\${AppName}\InstallDir"
  DeleteRegKey HKCU "SOFTWARE\${Company}\${AppName}\SettingDir"
  DeleteRegKey HKCU "SOFTWARE\${Company}\${AppName}"
  DeleteRegKey /ifempty HKCU "SOFTWARE\${Company}"
  DeleteRegKey HKLM "Software\Microsoft\Windows\CurrentVersion\Uninstall\${Company} ${AppName}"
SectionEnd