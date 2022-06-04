
#include <iostream>
#include <Windows.h>
#include <cstring>
#include <cmath>
#include <cstdlib>
#include <cstdio>
#include <string>
#include <fstream>

#pragma warning(disable: 4996)

int getWorkDir(char* szProgramPath)
{
	char szPath[_MAX_PATH] = { 0 };
	char szDrive[_MAX_DRIVE] = { 0 };
	char szDir[_MAX_DIR] = { 0 };
	char szFname[_MAX_FNAME] = { 0 };
	char szExt[_MAX_EXT] = { 0 };

	GetModuleFileNameA(NULL, szPath, sizeof(szPath));
	//ZeroMemory(szProgramPath, strlen(szProgramPath));
	_splitpath_s(szPath, szDrive, szDir, szFname, szExt);
	sprintf_s(szProgramPath, _MAX_PATH, "%s%s", szDrive, szDir);

	return 0;
}

bool isFileExists_stat(std::string& name) 
{
	struct stat buffer;
	return (stat(name.c_str(), &buffer) == 0);
}

int main()
{
	char Path[MAX_PATH + 1] = { 0 };
	char Patht[MAX_PATH + 1] = "";
	getWorkDir(Path);
	std::cout << Path << std::endl;
	std::string temps0 = "addon.ini";
	std::string configfile = Path + temps0;
	std::cout << configfile << std::endl;
	if (isFileExists_stat(configfile))
	{
		std::ifstream inf(configfile);
		std::string com;
		std::strcpy(Patht, Path);
		while (std::getline(inf, com))
		{
			std::strcpy(Patht, Path);
			if (com == "")
				continue;
			if (com == "Clock")
				ShellExecute(0, "open", std::strcat(Patht, "Clock"), NULL, NULL, SW_SHOWNORMAL);
			else if (com == "Panel")
				ShellExecute(0, "open", std::strcat(Patht, "Panel"), NULL, NULL, SW_SHOWNORMAL);
			else if (com == "SystemMonitor")
				ShellExecute(0, "open", std::strcat(Patht, "SystemMonitor"), NULL, NULL, SW_SHOWNORMAL);
			else if (com == "WeatherP")
				ShellExecute(0, "open", std::strcat(Patht, "WeatherP"), NULL, NULL, SW_SHOWNORMAL);
			else
			{
				std::string ss = std::strcat(Patht, com.c_str());
				if (isFileExists_stat(ss))
					ShellExecute(0, "open", std::strcat(Patht, com.c_str()), NULL, NULL, SW_SHOWNORMAL);
				else
					ShellExecute(0, "open", com.c_str(), NULL, Path, SW_SHOWNORMAL);
			}
			std::cout << com;
		}
	}
	else
	{
		std::cout << "File not Found";
		ShellExecute(0, "open", std::strcat(Path, "OnStart.exe"), NULL, NULL, SW_SHOWNORMAL);
	}
	return 0;
}
