# BitirmeProjesiSodexo

[Sodexo .Net Örnek Bitirme Projesi v2 (1).pdf](https://github.com/Semra4141/BitirmeProjesiSodexo/files/8022593/Sodexo.Net.Ornek.Bitirme.Projesi.v2.1.pdf)

## Çalıştırmak için

- Veri tabanı betiği çalıştırılarak gerekli veri tabanları ve tablolar oluşturulmalı.
  - Betik çalıştığı zaman "Final" ve "FinalHangfire" isimlerinde iki veri tabanı oluşmaktadır. Final veri tabanı içinde Brand, Category, Color, Email, Offer, Product, User, UsingStatus tabloları oluşur.
- Web projesindeki ve Hangfire projesindeki appsettings.json içindeki connection string'ler düzenlenmeli.
- Rabbitmq indirilmeli ve çalıştırılmalı.
- Web projesi ve Hangfire projesi ayrı ayrı koşturulmalı.

## Projede kullandığım yapılar

- Kimlik doğrulama : Jwt
- Girilen bilgilerin istenilen formata uygunluğunu kontrol : FluentValidator
- E-posta gönderimi :
  - Kuyruk : Rabbitmq
  - E-postaların arka planda kuyruktan çekilerek gönderilmesi : Hangfire
  - Gönderim: MailKit
- Veriye erişim : EntityFramework, GenericRepository ve UnitOfWork modelleri
