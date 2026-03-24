Câu 1:
- Ở trong file GameManager hàm Update đang chạy đến m_boardController.Update(); mặc dù trong file BoardController đã có hàm update chạy rồi, BoardController đã được tạo khi loadlevel(eLevelMode mode) rồi. Điều này khiến input xử lý 2 lần/frame, bị thừa, cho nên xóa dòng if (m_boardController != null) m_boardController.Update() đi.
- Biến event Action StateChangedAction trong file GameManager được đăng ký ở 2 file BoardController và UIMainManager, nhưng đang thiếu mất các trường hợp phải hủy đăng ký đi, ví dụ như khi 1 trong 2 bị destroy, GameManager vẫn còn giữ delegate tới object cũ, restart level nhiều lần dễ phát sinh callback không cần thiết. Thêm phần hủy đăng ký ở 2 file BoardController và UIMainManager để ở trong hàm onDestroy().
- Ở trong file BoardController trong update khi gọi câu lệnh if (Input.GetMouseButton(0) && m_isDragging) {var hit = Physics2D.Raycast(m_cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero); ...} điều này sẽ khiến việc mỗi frame tạo ra raycast 1 lần ảnh hưởng tới hiệu suất game, chỉ khi nào người chơi thực sự kéo tay thì mới tại ray kiểm tra. Tạo thêm biến vị trí khi mới nhấn và so sánh vị trí khi drag, khi 2 vị trí khác nhau mới tạo ray.
  
Câu 2:
- Tạo mới NormalItemSkinSetting(ScriptableObject) để lưu 7 sprite tương ứng với 7 loại mục thông thường.
- Thêm menu trong MainToolMenu.cs để tạo/mở NormalItemSkinSetting từ Game Tools.
- Thêm đường dẫn trong Constants.cs để lấy NormalItemSkinSetting từ thư mục Resources.
- Chỉnh sửa NormalItem.cs để lấy dữ liệu từ NormalItemSkinSetting và thay đổi sprite SpriteRenderertheo từng eNormalType.
- Gán 7 Sprite trong thư mục Textures/Fish vào trong normalitemskinsetting.asset.
- Thay đổi phần hiển thị của normal iteam theo dữ liệu được gán.
  
Câu 3:
- Thêm hệ thống BoardClone để lưu trạng thái board ban đầu ngay khi level được tạo xong và sẵn sàng chơi.
- Tạo BoardClone để lưu loại normal item của từng ô.
- Thêm chức năng tạo lại board từ BoardClone trong Board.cs và BoardController.cs.
- Thêm chức năng restart trong GameManager.cs để xóa level hiện tại và load lại đúng mode với đúng board ban đầu.
- Thêm nút và chức năng Restart trong UIPanelGame.cs và gọi hàm restart trong UIMainManager.cs.
- Khi bấm Restart, level được tạo lại đúng như màn chơi và các ô ban đầu trước khi người chơi thực hiện nước đi đầu tiên.
  
Câu 4:
- Chỉnh sửa hàm FillGapsWithNewItems() trong Board.cs để thay thế nguyên lý random cũ bằng nguyên lý chọn item có kiểm chọn lọc.
- Thêm hàm GetNeighbourTypes(Cell cell) để lấy danh sách type của 4 ô xung quanh và loại khỏi nhóm type có thể sinh ra.
- Thêm hàm AddNeighbourType(Cell neighbour, List<NormalItem.eNormalType> result) để kiểm tra từng ô xung quanh và chỉ lấy normal item type hợp lệ.
- Thêm hàm CountNormalItemsOnBoard() để đếm số lượng từng loại normal item hiện có trên board.
- Thêm hàm GetLeastUsedAvailableType(List<NormalItem.eNormalType> excludedTypes) để tìm ra item hợp lệ có số lượng ít nhất trên board.
- Nếu có nhiều loại cùng ít nhất, hàm sẽ chọn ngẫu nhiên trong nhóm đó.
  
Câu 5:

  Ưu điểm của dự án sau khi nghiên cứu :
  
  + Project được chia tách thành các đổi tượng rõ ràng, dễ kiểm soát: GameManager (Quản lý và điều phối tổng quan của game), BoardController+Board (Xử lý nguyên lý của gameplay như cách tạo combo, cách ăn được 1 dãy item giống nhau,cách spawm item,...), Cell+Item+NormalItem+BonusItem (Xử lý dữ liệu các ô trong bảng và các vật phẩm của ô), LevelMoves+LevelTime (cơ chế,chức năng, điều kiện của mỗi level), cuối cùng làI UIMainManager+các panel UI (xử lý các sự kiện UI của game)
  + Sử dụng event và eStateGame trong GameManager và các lớp UIMainManager, BoardController đăng ký tới nó để xử lý các state của game trong quá trình thao tác giúp cho game logic sạch, rõ ràng và dễ mở rộng sang các chức năng Manager khác
  + Sử dụng ScriptableObject để quản lý setting của game giúp game dễ kiểm soát và thay đổi
  + Board và BoardController được tách riêng, BoardController xử lý gameplay chính còn Board xử lý logic của bàn cơ ví dụ như fill, swap, match, shift, shuffle... Cách tách này giúp code sạch và dễ theo dõi hơn

  Nhược điểm của dự án:
  
  + Lỗi trong hàm LoadLevel(eLevelMode mode), phần mode == eLevelMode.TIMER vẫn đang setup là  m_levelCondition.Setup(m_gameSettings.LevelMoves, m_uiMenu.GetLevelConditionView(), this); khiến cho time của mode không chạy đúng (đã fix)
  + Trong lớp LevelTime ở hàm update đang có điều kiện  if (m_time <= -1f) {OnConditionComplete();} nhưng trong  UpdateText(); điều kiện lại là if (m_time < 0f) return;. Điều này khiến cho trong mode game play khi thời gian đã chạy về không nhưng game vẫn chưa dừng, vẫn delay khựng 1 lúc mới hiện ra UI gameOver (đã fix)
  + Trong BoardController.cs đang xử lý cả InputHander điều này khiến BoardCtr xử lý các việc thao tác người chơi, cả cơ chế của bảng, không được sạch. Nên tách việc xử lý handel với bảng ra 1 lớp riêng ví dụ BoardInputHandler
