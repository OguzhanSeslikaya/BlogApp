<h1>BlogApp</h1>
<p>BlogApp, .NET tarafında soğan mimarisi üzerine inşa edilmiş bir API uygulaması aracılığıyla dinamik web sayfaları oluşturmanızı sağlar. Ayrıca, kullanıcıları ve rolleri yönetebilir, rolleri .NET tarafında belirtilmiş endpointlere erişim yetkisi verebilir ve endpoint tabanlı yetkilendirmeler gerçekleştirebilirsiniz.</p>

<h2>Amaç</h2>
<p>Bu uygulama, kullanıcıların web sayfalarını dinamik olarak oluşturmasına ve yönetmesine olanak tanır. Ayrıca, yetkilendirme işlemlerini yönetmek için uygun bir API sunar.</p>

<h2>Özellikler</h2>
<p>API: API tarafında PostgreSQL veritabanı kullanılarak veri saklanır. Soğan mimarisi prensiplerine dayanarak oluşturulan API, dinamik web sayfaları oluşturmak için gerekli işlevselliği sağlar.</p>

<p>Client: Angular ile geliştirilen client uygulaması, kullanıcıların blog içeriklerini görüntülemelerini ve yönetmelerini sağlar.</p>

<h2>Yetkilendirme</h2>
<p>Yetkilendirme ve Rollere Göre Erişim: Kullanıcılar ve rolleri yönetebilirsiniz. .NET tarafında belirtilmiş endpointler için rolleri belirleyip erişim yetkilerini düzenleyebilirsiniz. Bu sayede, belirli rollerin belirli işlemleri gerçekleştirmesini sağlayabilirsiniz.</p>

<p>Admin Sayfası: İlk girişinizde, yetkili bir kullanıcı tarafından oluşturulmuş olan yazılar sizi karşılar. İlk kayıt olduğunuzda, admin sayfasına erişiminiz yoktur. Ancak bir yetkili kişi tarafından yetkilendirilirseniz, sadece yetkilendirildiğiniz işlemleri gerçekleştirebilirsiniz.</p>

<h2>Kullanılan Teknolojiler</h2>
<h3>API</h3>
<ul>
  <li>Fluent Validation</li>
  <li>Asp.net Core Identity</li>
  <li>PostgreSQL</li>
  <li>Entity Framework Core</li>
  <li>MediatR</li>
  <li>Serilog</li>
</ul>
<h3>Client</h3>
<ul>
  <li>Jquery</li>
  <li>Sun Editor</li>
  <li>Alertify</li>
  <li>NGX-Spinner</li>
</ul>

<h2>Daha Fazla Bilgi</h2>
<p>Uygulama hakkında daha detaylı bilgi edinmek için, kodları inceleyebilir veya profilimdeki iletişim bilgileri kısmından bana ulaşarak sorularınızı sorabilirsiniz.</p>
