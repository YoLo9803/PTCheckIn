Feature: EmployeeSide
	In order to 方便工時計算
	As a gss PT員工
	I want to 記下我的工作時數

Scenario: 登入網站成功
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	When 我按下登入按鈕
	Then 首頁的用戶名稱應該為王方顯

Scenario Outline: 登入網站失敗
	Given 我前往登入畫面
	And 輸入帳號 <userName>
	And 輸入密碼 <password>
	When 我按下登入按鈕
	Then 應該出現登入失敗視窗
	Examples: 
		| scenarios       | userName      | password      |
		| 輸入帳號不輸入密碼 | chris_fs_wang |               |
		| 輸入密碼不輸入帳號 |               | jenkins_test_pw     |
		| 輸入錯誤的密碼    | chris_fs_wang | wrongPassword |
		| 帳號密碼皆不輸入  |                |              |

Scenario: 快速簽到成功
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我點選快速簽到按鈕
	Then 應該出現快速簽到成功視窗
	Then 首頁時鐘的簽到時間應該為現在的伺服器時間

Scenario: 快速簽退成功
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我點選快速簽退按鈕
	Then 應該出現快速簽退成功視窗
	Then 首頁時鐘的簽退時間應該為現在的伺服器時間

Scenario: 快速簽到時間確認
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我點選快速簽到按鈕
	And 我按下OK按鈕
	And 我按下名為行事曆簽到的連結
	And 我點選這個月的簽到行事曆
	Then 簽到時間應該為現在的伺服器時間

Scenario: 快速簽退時間確認
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我點選快速簽退按鈕
	And 我按下OK按鈕
	And 我按下名為行事曆簽到的連結
	And 我點選這個月的簽到行事曆
	Then 簽退時間應該為現在的伺服器時間


Scenario: 手動簽到退成功(點選行事曆簽到連結)
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我按下名為行事曆簽到的連結
	And 我點選這個月的簽到行事曆
	And 我填入今天的簽到時間 10:00
	And 我填入今天的簽退時間 19:00
	And 我按下今日的送出按鈕
	And 我按下OK按鈕
	And 我按下名為行事曆簽到的連結
	And 我點選這個月的簽到行事曆
	Then 今天的簽到時間應該為 10:00
	Then 今天的簽退時間應該為 19:00

Scenario: 手動簽到退成功(點選GO按鈕)
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我按下馬上簽到GO按鈕
	And 我點選這個月的簽到行事曆
	And 我填入今天的簽到時間 09:00
	And 我填入今天的簽退時間 18:00
	And 我按下今日的送出按鈕
	And 我按下OK按鈕
	And 我按下名為行事曆簽到的連結
	And 我點選這個月的簽到行事曆
	Then 今天的簽到時間應該為 09:00
	Then 今天的簽退時間應該為 18:00

Scenario Outline: 手動簽到錯誤
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我按下名為行事曆簽到的連結
	And 我點選這個月的簽到行事曆
	And 我填入今天的簽到時間 <startTime>
	And 我填入今天的簽退時間 <endTime>
	#UI防呆, 會將第一次的錯誤填值清空
	And 我填入今天的簽退時間 <endTime>
	And 我按下今日的送出按鈕
	Then 應該出現簽到失敗視窗並顯示錯誤訊息 <errorMessage>
	Examples: 
		| startTime   | endTime     | errorMessage |
		| 13:00       | 09:00       | 簽退時間不能早於簽到時間 |
		| 12:00       | 25:00       | 簽退時間不能早於簽到時間 |
		| -13:00      | 11:00       | 簽到時間不得為空     |
		| 10:00       | 下午一點     | 簽到時間不得為空  |
		| one o'clock | two o'clock | 簽到時間不得為空     |
		|             | 18:00       | 簽到時間不得為空     |
		| 09:00       |             | 簽到時間不得為空     |

Scenario: 關於我頁面基本資料確認
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	When 我按下名為關於我的連結
	Then 中文姓名應該為 王方顯
	Then 員工編號應該為 3172
	Then 英文姓名應該為 Chris_FS Wang

Scenario Outline: 編輯支援BU成功
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我按下名為關於我的連結
	And 我填寫員工資料
		| 支援BU | 支援專案Leader | 支援專案名稱    |
		| <BU> | <leader>       | <projectName> |
	When 我按下修改按鈕
	Then 應該出現修改成功視窗
	Examples: 
		| BU							  | leader     | projectName |
		| QAC                 _品質保證中心 | DAVID_CHOU | autoTesting |

Scenario Outline: 編輯支援BU資料確認
	Given 我前往登入畫面
	And 輸入帳號 chris_fs_wang
	And 輸入密碼 jenkins_test_pw
	And 我按下登入按鈕
	And 我按下名為關於我的連結
	And 我填寫員工資料
		| 支援BU | 支援專案Leader | 支援專案名稱    |
		| <BU> | <leader>       | <projectName> |
	And 我按下修改按鈕
	And 我按下OK按鈕
	And 我按下名為首頁的連結
	And 我按下名為關於我的連結
	Then 關於我內的資料應該為
		| 支援BU | 支援專案Leader | 支援專案名稱    |
		| <BU> | <leader>       | <projectName> |
	Examples: 
		| BU							  | leader     | projectName |
		| QAC                 _品質保證中心 | DAVID_CHOU | autoTesting |



