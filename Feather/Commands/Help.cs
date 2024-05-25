﻿namespace Feather.Commands
{
    public class Help
    {
        public Help(string[] args)
        {
            Program.ConsoleReturn(Resources.Help.HelpMessage, true);
        }
    }
}

//feather init							v	# 현재 경로를 피더 저장소로 초기화
//feather init -m [MOUNT_PATH]				# 현재 경로를 피더 저장소로 초기화하며 백업을 [MOUNT_PATH]에 기록 
//feather init [PATH]						v	# [PATH]를 피더 저장소로 초기화
//feather init [PATH] -m [MOUNT_PATH]			# [PATH]를 피더 저장소로 초기화하며 백업을 [MOUNT_PATH]에 기록

//feather log [NAME]						v	# 버전 이름이 [NAME] 조건에 해당하는 버전의 구조를 보여줌
//feather log -a							v	# 모든 버전의 대략적인 구조를 보여줌
//feather log -i [INDEX]					v	# 버전 [INDEX]의 버전 구조를 보여줌
//feather log -i [INDEX_1]~[INDEX_2]		v	# 버전 [INDEX_1]부터 버전 [INDEX_2]까지의 버전들의 구조를 보여줌

//feather show -i [INDEX] [FILE_PATH]		v	# 버전 [INDEX]의 [FILE_PATH]에 위치한 파일의 내용을 출력

//feather pull -i [INDEX]					v	# 버전 [INDEX] 피더 저장소를 복사

//feather commit [NAME]					v	# 버전 이름이 [NAME]인 피더 버전을 등록(현재 버전의 자식)
//feather commit [NAME] -p [INDEX]		v	# 버전 [INDEX]의 자식으로 버전 이름이 [NAME]인 피더 버전을 등록

//feather del -i [INDEX]						# 버전 [INDEX]를 삭제feather init							v	# 현재 경로를 피더 저장소로 초기화
//feather init -m [MOUNT_PATH]				# 현재 경로를 피더 저장소로 초기화하며 백업을 [MOUNT_PATH]에 기록 
//feather init [PATH]						v	# [PATH]를 피더 저장소로 초기화
//feather init [PATH] -m [MOUNT_PATH]			# [PATH]를 피더 저장소로 초기화하며 백업을 [MOUNT_PATH]에 기록

//feather log [NAME]						v	# 버전 이름이 [NAME] 조건에 해당하는 버전의 구조를 보여줌
//feather log -a							v	# 모든 버전의 대략적인 구조를 보여줌
//feather log -i [INDEX]					v	# 버전 [INDEX]의 버전 구조를 보여줌
//feather log -i [INDEX_1]~[INDEX_2]		v	# 버전 [INDEX_1]부터 버전 [INDEX_2]까지의 버전들의 구조를 보여줌

//feather show -i [INDEX] [FILE_PATH]		v	# 버전 [INDEX]의 [FILE_PATH]에 위치한 파일의 내용을 출력

//feather pull -i [INDEX]					v	# 버전 [INDEX] 피더 저장소를 복사

//feather commit [NAME]					v	# 버전 이름이 [NAME]인 피더 버전을 등록(현재 버전의 자식)
//feather commit [NAME] -p [INDEX]		v	# 버전 [INDEX]의 자식으로 버전 이름이 [NAME]인 피더 버전을 등록

//feather del -i [INDEX]						# 버전 [INDEX]를 삭제