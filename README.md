# CagdasTas_UlukunShop_Eticaret

# Merhabalar

Bu proje Backend kısmında c# dili ve dotnet core , frontend kısmında ise Angular mimarisi kullanılarak hazırlanmıştır.

Bu projeyi hazırlama amacım, Ekim 2023'de başlamış olduğum BAU Bright fullstack yazılım eğitiminde bir proje hazırlayabilmek ve bunun yanı sıra kendime bir çok yeni yetenek katmak oldu.

Bu projede **Onion architecture**, **CQRS pattern**, **Mediator Pattern**, **Azure Blob storage**, **Table per Hierarchy**, **Facebook Authentication**, **Google Authentication**, **Serilog**, **Seq**, **SignalR**, **Angular Materials** gibi ve bir çok ismini buraya sığdıramayacağım Kütüphane, Teknoloji ve Tasarım örüntüsü kullandım.

# Çalıştırma

-Projeyi başlatabilmek için UlukunShopAPI/Presentation klasöründeki UlukunShopAPI.API Projesini terminalde açıp `dotnet watch run` komutunu vermeniz gerekmektedir.

-Angular Projesini başlatabilmek için angular cli yüklü olmalı ve projenin bulunduğu klasörde `ng serve` komutunu verirseniz proje http://localhost:4200 portundan ayağa kalkacaktır.

-uygulamada **tascagdas** kullanıcı adı ile yetkilendirme sistemine takılmadan her işlemi gerçekleştirebilir ve istediğiniz yetki ve rol ayarlarını oluşturabilirsiniz.

> Kullanıcı adı: tascagdas
> Parola: 12345
>
> Kullanıcı adı: tascagdas1
> Parola:12345

## Bilgilendirmeler

- Proje 15 Nisan 2024 tarihindeki versiyonu (commit halinde) sqlite veri tabanı ile çalışmaktadır.

- Projenin geliştirilmesi esnasında ben Postgresql kullandığım için bazı fonksiyonlarda aradaki ufak farklardan dolayı sorunlar çıkmakta. elimden geldiğince bu sorunları düzelttim.

- Projeyi kullanıcak kişiler için en büyük sorun azure blob storage kullanımında olmasıdır backend kısmında local storage ile azura arasındaki geçiş çok kolaydır. Fakat angularda db'den çekilen dosya isimlerinin başına azure blob storagea ait domain adresi eklenmekte (bunu zamanında quickfix olarak yapmıştım). Buna ileride daha güzel bir çözüm uygulayacağımdan

> Azure storage kullanmak isteyen kişilerin benimle iletişime geçmesi halinde AccountKey temin edebilirim.

**Ayrıca ilerleyen dönemlerde projede şuan için eksik olan ödeme altyapısı, barkod sistemi, mesajlaşma sistemi gibi özelliklerin ekleneceğini belirtmek isterim.**
