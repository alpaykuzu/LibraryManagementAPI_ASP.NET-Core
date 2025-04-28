# LibraryManagementAPI_ASP.NET-Core

Bu proje, bir kütüphane yönetim sistemi için geliştirilmiş bir RESTful API'dir. API, öğrencilerin kitap ödünç alma işlemlerini, kitaplar, yazarlar, kategoriler ve ödünç alma işlemleri (Enrollment) gibi temel verileri yönetir.

## Proje İçeriği

### 1. **Entities (Veri Modelleri)**

Projede aşağıdaki **Entities** (veri modelleri) kullanılmaktadır:

- **Author**: Yazar bilgilerini içerir. 
  - `One-to-Many` ilişki: Bir yazar birden fazla kitap yazabilir.
  
- **Book**: Kitap bilgilerini içerir. 
  - `One-to-Many` ilişki: Bir kitap bir yazara ve bir kategoriye aittir.
  - `Many-to-Many` ilişki: Bir kitap birden fazla öğrenci tarafından ödünç alınabilir.

- **Category**: Kitap kategorilerini içerir. 
  - `One-to-Many` ilişki: Bir kategori birden fazla kitap içerebilir.

- **Student**: Öğrenci bilgilerini içerir. 
  - `Many-to-Many` ilişki: Bir öğrenci birden fazla kitap ödünç alabilir.

- **Enrollment**: Öğrencinin kitap ödünç alma işlemlerini tutan ara tablo. 
  - `Many-to-Many` ilişki: Öğrenci ve Kitap arasında.

### 2. **DTOs (Data Transfer Objects)**

API'nin dışarıya veri göndermek için kullandığı **DTOs** şunlardır:

- **AuthorDto**: Yazar bilgilerini dışarıya göndermek için kullanılan DTO.
- **BookDto**: Kitap bilgilerini dışarıya göndermek için kullanılan DTO.
- **CategoryDto**: Kategori bilgilerini dışarıya göndermek için kullanılan DTO.
- **StudentDto**: Öğrenci bilgilerini dışarıya göndermek için kullanılan DTO.
- **EnrollmentDto**: Öğrenci kitap ödünç alma işlemini dışarıya göndermek için kullanılan DTO.

DTO'lar ayrıca `Create`, `Update` ve `Simple` gibi farklı kullanım senaryoları için de mevcuttur.

### 3. **Controllerlar ve API İşlevsellikleri**

Projede aşağıdaki controller'lar bulunmaktadır:

- **AuthorController**: Yazarlarla ilgili işlemler.
- **BookController**: Kitaplarla ilgili işlemler.
- **CategoryController**: Kategorilerle ilgili işlemler.
- **StudentController**: Öğrencilerle ilgili işlemler.
- **EnrollmentController**: Öğrencilerin kitap ödünç alma işlemleri.

Controller'lar RESTful API prensiplerine göre tasarlanmıştır ve temel CRUD işlemleri (Create, Read, Update, Delete) sağlanmaktadır.

### 4. **İlişkiler ve Veri Mantığı**

Projenin temel iş mantığı, **One-to-Many** ve **Many-to-Many** ilişkileri üzerinde şekillenmiştir:

- **One-to-Many** ilişkisi:
  - Bir yazar birden fazla kitap yazabilir.
  - Bir kategori birden fazla kitap içerebilir.

- **Many-to-Many** ilişkisi:
  - Bir kitap birden fazla öğrenci tarafından ödünç alınabilir.
  - Bir öğrenci birden fazla kitap ödünç alabilir.

Bu ilişkiler `Entity Framework Core` tarafından yönetilmektedir ve veritabanında `Foreign Key` ilişkileriyle sağlanmaktadır.

### 5. **Kullanılan Teknolojiler ve Kütüphaneler**

- **ASP.NET Core 8.0**: API'nin temel altyapısı.
- **Entity Framework Core 9.0.4**: Veritabanı işlemleri ve migration yönetimi.
- **Swashbuckle.AspNetCore**: Swagger entegrasyonu ve API dokümantasyonu.
- **AutoMapper**: DTO'lar ve Entity'ler arasında veri dönüşümü.
- **Microsoft.EntityFrameworkCore.SqlServer**: SQL Server veritabanı entegrasyonu.

### 6. **Veritabanı ve Migrationlar**

Projede **Entity Framework Core** kullanılarak veritabanı yönetimi sağlanmaktadır. Migration işlemi yapılmıştır ve veritabanı şeması **update-database** komutu ile güncellenebilir.

#### Migration ve Veritabanı Oluşturma Adımları

1. Projeyi **Visual Studio** veya **dotnet CLI** ile açın.
2. İlk olarak migration'ı uygulamak için terminal veya Package Manager Console (PMC) üzerinden aşağıdaki komutu çalıştırın:

```bash
dotnet ef database update
```

3. Bu komut, veritabanını güncelleyecek ve gerekli tabloları oluşturacaktır.

Veritabanı şeması, aşağıdaki tablo yapıları ile oluşturulacaktır:

- **Authors**: Yazarların bilgilerini tutar.
- **Books**: Kitapların bilgilerini tutar.
- **Categories**: Kategorilerin bilgilerini tutar.
- **Students**: Öğrencilerin bilgilerini tutar.
- **Enrollments**: Öğrencilerin kitap ödünç alma işlemlerini tutan ara tablo.

### 7. **Swagger ile API Testi**

Projede **Swagger** entegrasyonu bulunmaktadır. Uygulama çalıştırıldıktan sonra Swagger UI üzerinden API'yi test edebilirsiniz. Swagger, API endpoint'lerini ve dönen sonuçları görsel olarak keşfetmenizi sağlar.

Swagger'a erişmek için aşağıdaki URL'yi kullanabilirsiniz:

```
https://localhost:{port}/swagger
```

### 8. **Proje Yapılandırması**

Proje, **`appsettings.json`** dosyasındaki veritabanı bağlantı dizesine bağlıdır. Aşağıda örnek bir bağlantı dizesi verilmiştir:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=(localdb)\\MSSQLLocalDB;Database=BookManagementDb;Trusted_Connection=True;"
},
"Logging": {
  "LogLevel": {
    "Default": "Information",
    "Microsoft.AspNetCore": "Warning"
  }
},
"AllowedHosts": "*"
```

Bu bağlantı dizesi, SQL Server LocalDB kullanarak `BookManagementDb` veritabanına bağlanacaktır. SQL Server Object Explorer üzerinden bu veritabanını görüntüleyebilirsiniz.

### 9. **Proje Çalıştırma**

Projeyi çalıştırmak için:

1. **Visual Studio** üzerinden `F5` tuşuna basarak veya terminal üzerinden `dotnet run` komutunu çalıştırarak API'yi başlatabilirsiniz.
2. API, varsayılan olarak `https://localhost:{port}` adresinde çalışacaktır.
