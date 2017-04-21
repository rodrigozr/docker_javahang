FROM microsoft/windowsservercore

# Install PS Tools
COPY PSTools.zip c:/
RUN powershell Expand-Archive -Path C:\PSTools.zip -DestinationPath C:\PSTools \
    && powershell [Environment]::SetEnvironmentVariable(\"Path\", $env:Path + \";C:\\PSTools\", [EnvironmentVariableTarget]::Machine) \
    && C:\PSTools\pslist.exe /accepteula \
    && del /Q C:\PSTools.zip

# Install JAVA
COPY j2sdk-1_4_2_19-windows-i586-p.* c:/
RUN C:\j2sdk-1_4_2_19-windows-i586-p.cmd \
    && powershell [Environment]::SetEnvironmentVariable(\"Path\", $env:Path + \";C:\j2sdk1.4.2_19\jre\bin\", [EnvironmentVariableTarget]::Machine) \
    && del /Q C:\j2sdk-1_4_2_19-windows-i586-p.*

# Copy the hanger code
COPY HelloWorld.java  c:/

# Compile it
RUN C:\j2sdk1.4.2_19\bin\javac HelloWorld.java