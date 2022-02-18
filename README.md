# BitirmeProjesiSodexo

[Sodexo .Net Örnek Bitirme Projesi v2 (1).pdf](https://github.com/Semra4141/BitirmeProjesiSodexo/files/8022593/Sodexo.Net.Ornek.Bitirme.Projesi.v2.1.pdf)

## Çalıştırmak için

Visual Studio 2022, .Net 5.0 ve Sql Server 2019 kullandım.

- Sunucuda "Final" ve "FinalHangfire" isimlerinde iki veri tabanı oluşturun. "script.sql" dosyasını çalıştırın.
  - Betik çalıştığı zaman "Final" veri tabanı içinde Brand, Category, Color, Offer, Product, User, UsingStatus tabloları oluşur.
- Web projesindeki ve Hangfire projesindeki appsettings.json içindeki connection string'leri düzenleyin.
- Rabbitmq indirin ve çalıştırın.
- Web projesi ve Hangfire projelerini ayrı ayrı koşturun.

- E-posta gönderimini kullanmadığım bir hesaptan yaptım. Kullanıcı adım ve parola kodda gerekli yerde yazılı (Final.Project.Hangfire/Jobs/EmailConsumer.cs 63. satır). Ancak gmail hesabının gönderime izin vermesi gerekiyor. Bu izni açtım ama kullanılmadığında kendi kendine kapanabilir. Gerekirse giriş yaparak bu izni açabilirsiniz.
  - Kullanıcı adı: nursimacelik00@gmail.com
  - Parola: sodexobootcamp
- Google hesabınızı yönetin -> Güvenlik -> Daha az güvenli uygulama erişimi

## Projede kullandığım yapılar

- Kimlik doğrulama : Jwt
- Girilen bilgilerin istenilen formata uygunluğunu kontrol : FluentValidator
- E-posta gönderimi :
  - Kuyruk : Rabbitmq
  - E-postaların arka planda kuyruktan çekilerek gönderilmesi : Hangfire
  - Gönderim: MailKit
- Veriye erişim : EntityFramework, GenericRepository ve UnitOfWork modelleri


# Api
![Screenshot (14)](https://user-images.githubusercontent.com/33669453/154668708-7bc50a18-36c4-4b75-b7a4-a40b52c04e16.png)

![Screenshot (15)](https://user-images.githubusercontent.com/33669453/154668774-44e6162d-60fc-42cc-a679-bce988acfb98.png)

![Screenshot (17)](https://user-images.githubusercontent.com/33669453/154742535-847a68a3-49e6-42ff-85dd-471becbea9e1.png)

