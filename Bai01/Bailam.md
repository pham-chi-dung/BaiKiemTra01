Câu 1: Value Types vs. Reference Types (Stack vs. Heap)
Sự khác biệt cốt lõi nằm ở cơ chế quản lý bộ nhớ và cách dữ liệu được sao chép:
Value Types (Kiểu giá trị - int, float, bool, struct, enum):
Vùng nhớ: Được lưu trữ trực tiếp trên bộ nhớ Stack (trừ trường hợp nó là biến thành viên nằm bên trong một Class thì sẽ nằm trên Heap cùng Class đó).
Cơ chế: Lưu giá trị thực sự. Khi gán biến này cho biến khác, C# sẽ tạo ra một bản sao dữ liệu độc lập (Copy-by-value). Thay đổi biến này không ảnh hưởng đến biến kia.
Reference Types (Kiểu tham chiếu - class, interface, string, delegate, object):
Vùng nhớ: Dữ liệu thực sự (Object Data) được lưu trên Heap, còn Stack chỉ lưu địa chỉ con trỏ (Reference) trỏ đến vùng nhớ Heap đó.
Cơ chế: Khi gán biến này cho biến khác, C# chỉ sao chép địa chỉ tham chiếu (Copy-by-reference). Cả hai biến đều trỏ chung về một đối tượng trên Heap, nên thay đổi dữ liệu qua biến này sẽ làm biến kia thay đổi theo.
So sánh Stack và Heap:
Stack: Quản lý theo cơ chế LIFO (Last In, First Out), tốc độ truy xuất cực nhanh, dung lượng nhỏ, tự động giải phóng khi hàm kết thúc.
Heap: Dùng cho bộ nhớ động, dung lượng lớn nhưng truy xuất chậm hơn Stack. Việc giải phóng do Garbage Collector (GC) của .NET tự động đảm nhận.

Câu 2: Init-only Properties (init) trong C# 9/10
Khác biệt so với set thông thường:
set: Cho phép thay đổi giá trị thuộc tính ở bất kỳ thời điểm nào trong suốt vòng đời của đối tượng.
init: Chỉ cho phép gán giá trị duy nhất một lần khi khởi tạo đối tượng (thông qua Constructor hoặc cú pháp Object Initializer { Prop = val }). Sau khi đối tượng tạo xong, thuộc tính trở thành chỉ đọc (Read-only) và không thể thay đổi nữa.
Trường hợp sử dụng thực tế:
Dùng khi thiết kế các đối tượng bất biến (Immutable Objects) như DTO (Data Transfer Object), Config Models, hoặc Payload của Event/Message. Việc này giúp đảm bảo dữ liệu sau khi nhận từ API hoặc DB sẽ không vô tình bị một đoạn code logic khác chỉnh sửa làm sai lệch.
C#
public class UserDto
{
    public int Id { get; init; }
    public string Username { get; init; }
}

// Khởi tạo hợp lệ
var user = new UserDto { Id = 1, Username = "admin" }; 

// Báo lỗi biên dịch (Compile Error) ngay lập tức:
// user.Username = "hacker"; 

Câu 3: Phương thức virtual (lớp cha) vs. override (lớp con)
Trong tính Đa hình (Polymorphism):
virtual (ở Lớp cha): Khai báo một phương thức có cài đặt mặc định, đồng thời "mở cửa" cho phép các lớp con được quyền ghi đè lại logic nếu muốn. Nếu lớp con không ghi đè, nó sẽ dùng logic mặc định này.
override (ở Lớp con): Khai báo rằng lớp con đang thay thế hoàn toàn đoạn code xử lý của phương thức virtual từ lớp cha bằng logic riêng của nó.
Cơ chế đa hình lúc Runtime (Late Binding):
Khi bạn gọi phương thức thông qua con trỏ kiểu lớp cha nhưng đối tượng thực tế là lớp con, C# sẽ kiểm tra bảng ảo (vtable) và thực thi hàm có từ khóa override ở lớp con thay vì hàm virtual ở lớp cha.
C#
public class Animal {
    public virtual void MakeSound() => Console.WriteLine("Animal sound");
}

public class Dog : Animal {
    public override void MakeSound() => Console.WriteLine("Woof!");
}

// Gọi thực thi
Animal myPet = new Dog();
myPet.MakeSound(); // Kết quả in ra "Woof!" nhờ tính Đa hình

Câu 4: Tại sao static không thể truy xuất qua một thể hiện (Object Instance)?
Thành phần khai báo static (field, property, method) thuộc về cấp độ Lớp (Class-level / Type Metadata) chứ không thuộc về từng Thể hiện đối tượng (Instance-level) được tạo ra bằng new.
Các lý do C# bắt buộc truy xuất static qua tên Lớp (ClassName.StaticMember):
1.	Bản chất quản lý bộ nhớ: Thành phần static chỉ được cấp phát một vùng nhớ duy nhất cho toàn bộ ứng dụng ngay khi Class được load. Việc tạo ra $100$ instance bằng new cũng không làm tăng thêm vùng nhớ static. Do đó, nó không hề nằm trong cấu trúc bộ nhớ (memory layout) của bất kỳ instance nào.
2.	Tránh nhầm lẫn về mặt ngữ nghĩa (Code Clarity): Nếu cho phép gọi instance.StaticMethod(), lập trình viên dễ hiểu nhầm rằng phương thức đó đang thao tác trên dữ liệu riêng của instance đó.
3.	An toàn ngữ thi (Null Safety): Gọi static qua tên Lớp giúp trình biên dịch không cần sinh code kiểm tra con trỏ null (null check) trên instance, tối ưu hiệu năng và tránh lỗi NullReferenceException.


