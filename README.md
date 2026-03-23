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
  
