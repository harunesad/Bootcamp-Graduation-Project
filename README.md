# Bootcamp-Graduation--Project
 Bir savaş oyunu.
 Oyun ana ekranda oyuncunun parası bulunuyor. Bu para ile yakın mesafe ya da uzak mesafe birlikler satın alabiliyoruz.
 Satın alınan karakterler bir particle ile spawn olacak.
 Daha sonrasında iki yakın mesafe birliği birleştirdiğimizde(merge mekaniği) yakın birliğin bir üst leveli sahnemize spawn oluyor.
 Aynı olay uzak mesafe birlikler için de geçerli. Birlikleri bu şekilde birleştirerek sürekli olarak birliklerimizi üst levellere yükseltiyoruz.
 
 Ana ekranda en başta karşımızda da düşman birlikler olacak. Bu birliklerin seviyesi oyun leveli arttıkça artıyor olacak.
 Bütün karakterlerin kendine göre health, armor, attack değişkenleri olacak.
 Bu şekilde de savaş ekranı şekillenecek.
 
 Gelelim savaş ekranına,
 ana ekranda battle butonuna bastığımızda savaş ekranına geçiyoruz.
 Savaş ekranında ise her birlik en yakınındaki rakibini belirleyip ona kilitleniyor.
 Yani kitlendiği rakibe dönüp, ona doğru ilerleyip, ona saldıracak.
 Daha sonra rakibi ölünce kendine yeni rakip arayacak ve yeni rakibine kilitlenecek ta ki sahnede rakibi kalmayıncaya dek.
 
 Kazanma kaybetme ekranında ise,
 Eğer sahnede enemy kalmazsa biz kazanıyoruz ve kazandığımızı gösteren bir ui oluşuyor, ui da kazanığımız paryı gösteriyor ve 
 bir üst levele geçmek için bir buton geliyor.
 Eğer sahnede bizim hiç askerimiz kalmazsa kaybediyoruz ve kaybettiğimizi gösteren bir ui oluşuyor ve aynı lelevi tekrar etmek için bir buton oluşuyor.
