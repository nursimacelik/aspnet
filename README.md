# BitirmeProjesiSodexo

[Sodexo .Net Örnek Bitirme Projesi v2 (1).pdf](https://github.com/Semra4141/BitirmeProjesiSodexo/files/8022593/Sodexo.Net.Ornek.Bitirme.Projesi.v2.1.pdf)

## Çalıştırmak için

Visual Studio 2022, .Net 5.0 ve Sql Server 2019 kullandım.

- Sunucuda "Final" ve "FinalHangfire" isimlerinde iki veri tabanı oluşturun. "script.sql" dosyasını çalıştırın.
  - Betik çalıştığı zaman "Final" veri tabanı içinde Brand, Category, Color, Offer, Product, User, UsingStatus tabloları oluşur.
- Web projesindeki ve Hangfire projesindeki appsettings.json içindeki ConnectionStrings alanının değerini düzenleyin.
- Web projesindeki appsettings.json içindeki UploadsPath alanının değerini düzenleyin. Yüklenen ürün görselleri burada belirtilen klasöre kaydedilecektir.
- Rabbitmq indirin ve çalıştırın.
- Web projesi ve Hangfire projelerini ayrı ayrı koşturun.


## Projede kullandığım yapılar

- Kimlik doğrulama : Jwt
- Girilen bilgilerin istenilen formata uygunluğunu kontrol : FluentValidator
- E-posta gönderimi :
  - Kuyruk : Rabbitmq
  - E-postaların arka planda kuyruktan çekilerek gönderilmesi : Hangfire
  - Gönderim: MailKit
- Veriye erişim : EntityFramework, GenericRepository ve UnitOfWork modelleri


## Notlar

- Projede ek olarak parola değiştirme imkanı bulunuyor.
- Ürün görselini Swagger'dan dosya yükleme ile almayı hedefledim. Yüklenen dosyayı sunucuda bir klasöre kaydedip dosya yolunu veri tabanına kaydediyorum. Dosyayı FromForm şeklinde alabildim. Fakat ürün eklemede aynı endpointte hem FromBody hem FromForm kullanmak sorun çıkardı. Bu sebeple ürün oluşturulurken görseli alamadım. Onun yerine /UpdateImage/{productId} şeklinde bir endpoint ekledim. Bu endpointten ürünlerin görselleri yüklenebilir.
- Kullanıcının yaptığı teklifler /Offer altında, kendisine gelen teklifler ise /IncomingOffer altında değerlendiriliyor.
- Kullanıcı yaptığı teklifi Delete /Offer/{id} ile geri çekebilir.
- Satın alma endpointi ürün id'si alıyor. Eğer kullanıcının o ürün için kabul edilmiş bir teklifi varsa o teklife göre fiyatı belirliyor. Yoksa, olduğu fiyattan satıyor.
- Gelen teklifler Put /IncomingOffer/{id} ile cevaplanır.

# Api
![Screenshot (14)](https://user-images.githubusercontent.com/33669453/154668708-7bc50a18-36c4-4b75-b7a4-a40b52c04e16.png)

![Screenshot (15)](https://user-images.githubusercontent.com/33669453/154668774-44e6162d-60fc-42cc-a679-bce988acfb98.png)

![Screenshot (17)](https://user-images.githubusercontent.com/33669453/154742535-847a68a3-49e6-42ff-85dd-471becbea9e1.png)

