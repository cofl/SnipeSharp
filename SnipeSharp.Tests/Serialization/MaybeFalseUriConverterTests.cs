using System;
using System.IO;
using Newtonsoft.Json;
using SnipeSharp.Serialization;
using SnipeSharp.Serialization.Converters;
using Xunit;

namespace SnipeSharp.Tests
{
    public sealed class MaybeFalseUriConverterTests
    {
        [Theory]
        [InlineData("https://example.org", "\"https://example.org\"")]
        [InlineData("http://localhost", "\"http://localhost\"")]
        [InlineData("https://localhost:8081", "\"https://localhost:8081\"")]
        // next 15 data from mockaroo
        [InlineData("http://discuz.net/amet/eleifend.html?elit=nulla&proin=pede&interdum=ullamcorper&mauris=augue&non=a&ligula=suscipit&pellentesque=nulla&ultrices=elit&phasellus=ac&id=nulla&sapien=sed&in=vel&sapien=enim&iaculis=sit&congue=amet&vivamus=nunc&metus=viverra&arcu=dapibus&adipiscing=nulla&molestie=suscipit&hendrerit=ligula&at=in&vulputate=lacus&vitae=curabitur&nisl=at&aenean=ipsum&lectus=ac&pellentesque=tellus&eget=semper&nunc=interdum&donec=mauris&quis=ullamcorper&orci=purus&eget=sit", "\"http://discuz.net/amet/eleifend.html?elit=nulla&proin=pede&interdum=ullamcorper&mauris=augue&non=a&ligula=suscipit&pellentesque=nulla&ultrices=elit&phasellus=ac&id=nulla&sapien=sed&in=vel&sapien=enim&iaculis=sit&congue=amet&vivamus=nunc&metus=viverra&arcu=dapibus&adipiscing=nulla&molestie=suscipit&hendrerit=ligula&at=in&vulputate=lacus&vitae=curabitur&nisl=at&aenean=ipsum&lectus=ac&pellentesque=tellus&eget=semper&nunc=interdum&donec=mauris&quis=ullamcorper&orci=purus&eget=sit\"")]
        [InlineData("https://w3.org/libero/convallis/eget.xml?porttitor=velit&pede=nec&justo=nisi&eu=vulputate&massa=nonummy&donec=maecenas&dapibus=tincidunt&duis=lacus&at=at&velit=velit&eu=vivamus&est=vel&congue=nulla&elementum=eget&in=eros&hac=elementum&habitasse=pellentesque&platea=quisque&dictumst=porta&morbi=volutpat&vestibulum=erat&velit=quisque&id=erat&pretium=eros&iaculis=viverra&diam=eget&erat=congue&fermentum=eget&justo=semper&nec=rutrum&condimentum=nulla&neque=nunc&sapien=purus&placerat=phasellus&ante=in&nulla=felis&justo=donec&aliquam=semper&quis=sapien&turpis=a&eget=libero&elit=nam&sodales=dui&scelerisque=proin&mauris=leo&sit=odio&amet=porttitor&eros=id&suspendisse=consequat&accumsan=in&tortor=consequat&quis=ut&turpis=nulla&sed=sed&ante=accumsan&vivamus=felis&tortor=ut", "\"https://w3.org/libero/convallis/eget.xml?porttitor=velit&pede=nec&justo=nisi&eu=vulputate&massa=nonummy&donec=maecenas&dapibus=tincidunt&duis=lacus&at=at&velit=velit&eu=vivamus&est=vel&congue=nulla&elementum=eget&in=eros&hac=elementum&habitasse=pellentesque&platea=quisque&dictumst=porta&morbi=volutpat&vestibulum=erat&velit=quisque&id=erat&pretium=eros&iaculis=viverra&diam=eget&erat=congue&fermentum=eget&justo=semper&nec=rutrum&condimentum=nulla&neque=nunc&sapien=purus&placerat=phasellus&ante=in&nulla=felis&justo=donec&aliquam=semper&quis=sapien&turpis=a&eget=libero&elit=nam&sodales=dui&scelerisque=proin&mauris=leo&sit=odio&amet=porttitor&eros=id&suspendisse=consequat&accumsan=in&tortor=consequat&quis=ut&turpis=nulla&sed=sed&ante=accumsan&vivamus=felis&tortor=ut\"")]
        [InlineData("http://msu.edu/scelerisque/quam/turpis/adipiscing/lorem.js?orci=vestibulum&pede=eget&venenatis=vulputate&non=ut&sodales=ultrices&sed=vel&tincidunt=augue&eu=vestibulum&felis=ante&fusce=ipsum&posuere=primis&felis=in", "\"http://msu.edu/scelerisque/quam/turpis/adipiscing/lorem.js?orci=vestibulum&pede=eget&venenatis=vulputate&non=ut&sodales=ultrices&sed=vel&tincidunt=augue&eu=vestibulum&felis=ante&fusce=ipsum&posuere=primis&felis=in\"")]
        [InlineData("https://ovh.net/semper/rutrum/nulla/nunc.json?faucibus=diam&accumsan=cras&odio=pellentesque&curabitur=volutpat&convallis=dui&duis=maecenas&consequat=tristique&dui=est&nec=et&nisi=tempus&volutpat=semper&eleifend=est&donec=quam&ut=pharetra&dolor=magna&morbi=ac&vel=consequat&lectus=metus&in=sapien&quam=ut&fringilla=nunc&rhoncus=vestibulum&mauris=ante&enim=ipsum&leo=primis&rhoncus=in&sed=faucibus&vestibulum=orci&sit=luctus&amet=et&cursus=ultrices&id=posuere&turpis=cubilia&integer=curae&aliquet=mauris&massa=viverra&id=diam&lobortis=vitae&convallis=quam&tortor=suspendisse&risus=potenti&dapibus=nullam&augue=porttitor&vel=lacus&accumsan=at&tellus=turpis&nisi=donec&eu=posuere&orci=metus&mauris=vitae&lacinia=ipsum&sapien=aliquam&quis=non&libero=mauris&nullam=morbi&sit=non&amet=lectus&turpis=aliquam&elementum=sit&ligula=amet&vehicula=diam&consequat=in&morbi=magna&a=bibendum&ipsum=imperdiet&integer=nullam&a=orci&nibh=pede&in=venenatis&quis=non&justo=sodales&maecenas=sed&rhoncus=tincidunt&aliquam=eu&lacus=felis&morbi=fusce", "\"https://ovh.net/semper/rutrum/nulla/nunc.json?faucibus=diam&accumsan=cras&odio=pellentesque&curabitur=volutpat&convallis=dui&duis=maecenas&consequat=tristique&dui=est&nec=et&nisi=tempus&volutpat=semper&eleifend=est&donec=quam&ut=pharetra&dolor=magna&morbi=ac&vel=consequat&lectus=metus&in=sapien&quam=ut&fringilla=nunc&rhoncus=vestibulum&mauris=ante&enim=ipsum&leo=primis&rhoncus=in&sed=faucibus&vestibulum=orci&sit=luctus&amet=et&cursus=ultrices&id=posuere&turpis=cubilia&integer=curae&aliquet=mauris&massa=viverra&id=diam&lobortis=vitae&convallis=quam&tortor=suspendisse&risus=potenti&dapibus=nullam&augue=porttitor&vel=lacus&accumsan=at&tellus=turpis&nisi=donec&eu=posuere&orci=metus&mauris=vitae&lacinia=ipsum&sapien=aliquam&quis=non&libero=mauris&nullam=morbi&sit=non&amet=lectus&turpis=aliquam&elementum=sit&ligula=amet&vehicula=diam&consequat=in&morbi=magna&a=bibendum&ipsum=imperdiet&integer=nullam&a=orci&nibh=pede&in=venenatis&quis=non&justo=sodales&maecenas=sed&rhoncus=tincidunt&aliquam=eu&lacus=felis&morbi=fusce\"")]
        [InlineData("http://unblog.fr/malesuada/in/imperdiet/et/commodo.json?sit=vestibulum&amet=aliquet&consectetuer=ultrices&adipiscing=erat&elit=tortor&proin=sollicitudin&interdum=mi&mauris=sit&non=amet&ligula=lobortis&pellentesque=sapien&ultrices=sapien&phasellus=non&id=mi&sapien=integer&in=ac&sapien=neque&iaculis=duis&congue=bibendum&vivamus=morbi&metus=non&arcu=quam&adipiscing=nec&molestie=dui&hendrerit=luctus&at=rutrum&vulputate=nulla&vitae=tellus&nisl=in&aenean=sagittis&lectus=dui&pellentesque=vel&eget=nisl&nunc=duis&donec=ac&quis=nibh&orci=fusce&eget=lacus&orci=purus&vehicula=aliquet&condimentum=at&curabitur=feugiat&in=non&libero=pretium&ut=quis&massa=lectus&volutpat=suspendisse&convallis=potenti&morbi=in&odio=eleifend&odio=quam&elementum=a&eu=odio&interdum=in&eu=hac&tincidunt=habitasse&in=platea&leo=dictumst&maecenas=maecenas&pulvinar=ut&lobortis=massa&est=quis&phasellus=augue&sit=luctus&amet=tincidunt&erat=nulla&nulla=mollis&tempus=molestie&vivamus=lorem&in=quisque&felis=ut&eu=erat&sapien=curabitur&cursus=gravida&vestibulum=nisi&proin=at&eu=nibh&mi=in&nulla=hac&ac=habitasse&enim=platea&in=dictumst&tempor=aliquam&turpis=augue&nec=quam&euismod=sollicitudin&scelerisque=vitae&quam=consectetuer&turpis=eget&adipiscing=rutrum&lorem=at&vitae=lorem&mattis=integer&nibh=tincidunt&ligula=ante&nec=vel&sem=ipsum&duis=praesent&aliquam=blandit&convallis=lacinia", "\"http://unblog.fr/malesuada/in/imperdiet/et/commodo.json?sit=vestibulum&amet=aliquet&consectetuer=ultrices&adipiscing=erat&elit=tortor&proin=sollicitudin&interdum=mi&mauris=sit&non=amet&ligula=lobortis&pellentesque=sapien&ultrices=sapien&phasellus=non&id=mi&sapien=integer&in=ac&sapien=neque&iaculis=duis&congue=bibendum&vivamus=morbi&metus=non&arcu=quam&adipiscing=nec&molestie=dui&hendrerit=luctus&at=rutrum&vulputate=nulla&vitae=tellus&nisl=in&aenean=sagittis&lectus=dui&pellentesque=vel&eget=nisl&nunc=duis&donec=ac&quis=nibh&orci=fusce&eget=lacus&orci=purus&vehicula=aliquet&condimentum=at&curabitur=feugiat&in=non&libero=pretium&ut=quis&massa=lectus&volutpat=suspendisse&convallis=potenti&morbi=in&odio=eleifend&odio=quam&elementum=a&eu=odio&interdum=in&eu=hac&tincidunt=habitasse&in=platea&leo=dictumst&maecenas=maecenas&pulvinar=ut&lobortis=massa&est=quis&phasellus=augue&sit=luctus&amet=tincidunt&erat=nulla&nulla=mollis&tempus=molestie&vivamus=lorem&in=quisque&felis=ut&eu=erat&sapien=curabitur&cursus=gravida&vestibulum=nisi&proin=at&eu=nibh&mi=in&nulla=hac&ac=habitasse&enim=platea&in=dictumst&tempor=aliquam&turpis=augue&nec=quam&euismod=sollicitudin&scelerisque=vitae&quam=consectetuer&turpis=eget&adipiscing=rutrum&lorem=at&vitae=lorem&mattis=integer&nibh=tincidunt&ligula=ante&nec=vel&sem=ipsum&duis=praesent&aliquam=blandit&convallis=lacinia\"")]
        [InlineData("https://deliciousdays.com/vel/pede/morbi/porttitor/lorem/id.json?vivamus=nam&tortor=nulla&duis=integer&mattis=pede&egestas=justo&metus=lacinia&aenean=eget&fermentum=tincidunt&donec=eget&ut=tempus&mauris=vel&eget=pede&massa=morbi&tempor=porttitor&convallis=lorem&nulla=id&neque=ligula&libero=suspendisse&convallis=ornare&eget=consequat&eleifend=lectus&luctus=in&ultricies=est&eu=risus&nibh=auctor&quisque=sed&id=tristique&justo=in&sit=tempus&amet=sit&sapien=amet&dignissim=sem", "\"https://deliciousdays.com/vel/pede/morbi/porttitor/lorem/id.json?vivamus=nam&tortor=nulla&duis=integer&mattis=pede&egestas=justo&metus=lacinia&aenean=eget&fermentum=tincidunt&donec=eget&ut=tempus&mauris=vel&eget=pede&massa=morbi&tempor=porttitor&convallis=lorem&nulla=id&neque=ligula&libero=suspendisse&convallis=ornare&eget=consequat&eleifend=lectus&luctus=in&ultricies=est&eu=risus&nibh=auctor&quisque=sed&id=tristique&justo=in&sit=tempus&amet=sit&sapien=amet&dignissim=sem\"")]
        [InlineData("http://time.com/duis/faucibus/accumsan.jsp?molestie=tortor&hendrerit=sollicitudin&at=mi&vulputate=sit&vitae=amet&nisl=lobortis&aenean=sapien&lectus=sapien&pellentesque=non&eget=mi&nunc=integer&donec=ac&quis=neque&orci=duis&eget=bibendum&orci=morbi&vehicula=non&condimentum=quam&curabitur=nec&in=dui&libero=luctus&ut=rutrum&massa=nulla&volutpat=tellus&convallis=in&morbi=sagittis&odio=dui&odio=vel&elementum=nisl&eu=duis&interdum=ac&eu=nibh&tincidunt=fusce&in=lacus&leo=purus&maecenas=aliquet&pulvinar=at&lobortis=feugiat&est=non&phasellus=pretium&sit=quis&amet=lectus&erat=suspendisse&nulla=potenti&tempus=in&vivamus=eleifend&in=quam&felis=a&eu=odio&sapien=in&cursus=hac&vestibulum=habitasse&proin=platea", "\"http://time.com/duis/faucibus/accumsan.jsp?molestie=tortor&hendrerit=sollicitudin&at=mi&vulputate=sit&vitae=amet&nisl=lobortis&aenean=sapien&lectus=sapien&pellentesque=non&eget=mi&nunc=integer&donec=ac&quis=neque&orci=duis&eget=bibendum&orci=morbi&vehicula=non&condimentum=quam&curabitur=nec&in=dui&libero=luctus&ut=rutrum&massa=nulla&volutpat=tellus&convallis=in&morbi=sagittis&odio=dui&odio=vel&elementum=nisl&eu=duis&interdum=ac&eu=nibh&tincidunt=fusce&in=lacus&leo=purus&maecenas=aliquet&pulvinar=at&lobortis=feugiat&est=non&phasellus=pretium&sit=quis&amet=lectus&erat=suspendisse&nulla=potenti&tempus=in&vivamus=eleifend&in=quam&felis=a&eu=odio&sapien=in&cursus=hac&vestibulum=habitasse&proin=platea\"")]
        [InlineData("http://usda.gov/natoque/penatibus/et/magnis.json?sed=platea&sagittis=dictumst&nam=morbi&congue=vestibulum&risus=velit&semper=id&porta=pretium&volutpat=iaculis&quam=diam&pede=erat&lobortis=fermentum&ligula=justo&sit=nec&amet=condimentum&eleifend=neque&pede=sapien&libero=placerat&quis=ante&orci=nulla&nullam=justo&molestie=aliquam&nibh=quis&in=turpis&lectus=eget&pellentesque=elit&at=sodales&nulla=scelerisque&suspendisse=mauris&potenti=sit&cras=amet&in=eros&purus=suspendisse&eu=accumsan&magna=tortor&vulputate=quis&luctus=turpis&cum=sed&sociis=ante", "\"http://usda.gov/natoque/penatibus/et/magnis.json?sed=platea&sagittis=dictumst&nam=morbi&congue=vestibulum&risus=velit&semper=id&porta=pretium&volutpat=iaculis&quam=diam&pede=erat&lobortis=fermentum&ligula=justo&sit=nec&amet=condimentum&eleifend=neque&pede=sapien&libero=placerat&quis=ante&orci=nulla&nullam=justo&molestie=aliquam&nibh=quis&in=turpis&lectus=eget&pellentesque=elit&at=sodales&nulla=scelerisque&suspendisse=mauris&potenti=sit&cras=amet&in=eros&purus=suspendisse&eu=accumsan&magna=tortor&vulputate=quis&luctus=turpis&cum=sed&sociis=ante\"")]
        [InlineData("http://deviantart.com/dolor/sit/amet.jsp?libero=nibh&nam=fusce&dui=lacus&proin=purus&leo=aliquet&odio=at&porttitor=feugiat&id=non&consequat=pretium&in=quis&consequat=lectus&ut=suspendisse&nulla=potenti&sed=in", "\"http://deviantart.com/dolor/sit/amet.jsp?libero=nibh&nam=fusce&dui=lacus&proin=purus&leo=aliquet&odio=at&porttitor=feugiat&id=non&consequat=pretium&in=quis&consequat=lectus&ut=suspendisse&nulla=potenti&sed=in\"")]
        [InlineData("https://networkadvertising.org/dui/vel/nisl.aspx?in=luctus&magna=nec&bibendum=molestie&imperdiet=sed&nullam=justo&orci=pellentesque&pede=viverra&venenatis=pede&non=ac&sodales=diam&sed=cras&tincidunt=pellentesque&eu=volutpat&felis=dui&fusce=maecenas&posuere=tristique&felis=est&sed=et&lacus=tempus&morbi=semper&sem=est&mauris=quam&laoreet=pharetra&ut=magna&rhoncus=ac&aliquet=consequat&pulvinar=metus&sed=sapien&nisl=ut&nunc=nunc&rhoncus=vestibulum&dui=ante&vel=ipsum&sem=primis&sed=in&sagittis=faucibus&nam=orci&congue=luctus&risus=et&semper=ultrices&porta=posuere&volutpat=cubilia&quam=curae&pede=mauris&lobortis=viverra&ligula=diam&sit=vitae&amet=quam", "\"https://networkadvertising.org/dui/vel/nisl.aspx?in=luctus&magna=nec&bibendum=molestie&imperdiet=sed&nullam=justo&orci=pellentesque&pede=viverra&venenatis=pede&non=ac&sodales=diam&sed=cras&tincidunt=pellentesque&eu=volutpat&felis=dui&fusce=maecenas&posuere=tristique&felis=est&sed=et&lacus=tempus&morbi=semper&sem=est&mauris=quam&laoreet=pharetra&ut=magna&rhoncus=ac&aliquet=consequat&pulvinar=metus&sed=sapien&nisl=ut&nunc=nunc&rhoncus=vestibulum&dui=ante&vel=ipsum&sem=primis&sed=in&sagittis=faucibus&nam=orci&congue=luctus&risus=et&semper=ultrices&porta=posuere&volutpat=cubilia&quam=curae&pede=mauris&lobortis=viverra&ligula=diam&sit=vitae&amet=quam\"")]
        [InlineData("http://state.gov/at.js?luctus=donec&nec=ut&molestie=mauris&sed=eget&justo=massa&pellentesque=tempor&viverra=convallis&pede=nulla&ac=neque&diam=libero&cras=convallis&pellentesque=eget&volutpat=eleifend&dui=luctus&maecenas=ultricies&tristique=eu&est=nibh&et=quisque&tempus=id&semper=justo&est=sit&quam=amet&pharetra=sapien&magna=dignissim&ac=vestibulum&consequat=vestibulum&metus=ante&sapien=ipsum&ut=primis&nunc=in&vestibulum=faucibus", "\"http://state.gov/at.js?luctus=donec&nec=ut&molestie=mauris&sed=eget&justo=massa&pellentesque=tempor&viverra=convallis&pede=nulla&ac=neque&diam=libero&cras=convallis&pellentesque=eget&volutpat=eleifend&dui=luctus&maecenas=ultricies&tristique=eu&est=nibh&et=quisque&tempus=id&semper=justo&est=sit&quam=amet&pharetra=sapien&magna=dignissim&ac=vestibulum&consequat=vestibulum&metus=ante&sapien=ipsum&ut=primis&nunc=in&vestibulum=faucibus\"")]
        [InlineData("https://goodreads.com/nam.xml?vulputate=ut&elementum=massa&nullam=volutpat&varius=convallis&nulla=morbi&facilisi=odio&cras=odio&non=elementum&velit=eu&nec=interdum&nisi=eu&vulputate=tincidunt&nonummy=in&maecenas=leo&tincidunt=maecenas&lacus=pulvinar&at=lobortis&velit=est&vivamus=phasellus&vel=sit&nulla=amet&eget=erat&eros=nulla&elementum=tempus&pellentesque=vivamus&quisque=in&porta=felis&volutpat=eu&erat=sapien&quisque=cursus&erat=vestibulum&eros=proin&viverra=eu&eget=mi&congue=nulla&eget=ac&semper=enim&rutrum=in&nulla=tempor&nunc=turpis&purus=nec&phasellus=euismod&in=scelerisque&felis=quam&donec=turpis&semper=adipiscing&sapien=lorem&a=vitae&libero=mattis&nam=nibh&dui=ligula&proin=nec&leo=sem&odio=duis&porttitor=aliquam&id=convallis&consequat=nunc&in=proin&consequat=at&ut=turpis&nulla=a&sed=pede&accumsan=posuere&felis=nonummy&ut=integer&at=non&dolor=velit&quis=donec", "\"https://goodreads.com/nam.xml?vulputate=ut&elementum=massa&nullam=volutpat&varius=convallis&nulla=morbi&facilisi=odio&cras=odio&non=elementum&velit=eu&nec=interdum&nisi=eu&vulputate=tincidunt&nonummy=in&maecenas=leo&tincidunt=maecenas&lacus=pulvinar&at=lobortis&velit=est&vivamus=phasellus&vel=sit&nulla=amet&eget=erat&eros=nulla&elementum=tempus&pellentesque=vivamus&quisque=in&porta=felis&volutpat=eu&erat=sapien&quisque=cursus&erat=vestibulum&eros=proin&viverra=eu&eget=mi&congue=nulla&eget=ac&semper=enim&rutrum=in&nulla=tempor&nunc=turpis&purus=nec&phasellus=euismod&in=scelerisque&felis=quam&donec=turpis&semper=adipiscing&sapien=lorem&a=vitae&libero=mattis&nam=nibh&dui=ligula&proin=nec&leo=sem&odio=duis&porttitor=aliquam&id=convallis&consequat=nunc&in=proin&consequat=at&ut=turpis&nulla=a&sed=pede&accumsan=posuere&felis=nonummy&ut=integer&at=non&dolor=velit&quis=donec\"")]
        [InlineData("https://t.co/amet/cursus/id/turpis/integer/aliquet/massa.aspx?ligula=non&nec=mauris&sem=morbi&duis=non&aliquam=lectus&convallis=aliquam&nunc=sit&proin=amet&at=diam&turpis=in&a=magna&pede=bibendum&posuere=imperdiet&nonummy=nullam&integer=orci&non=pede&velit=venenatis&donec=non&diam=sodales&neque=sed&vestibulum=tincidunt&eget=eu&vulputate=felis&ut=fusce&ultrices=posuere&vel=felis&augue=sed&vestibulum=lacus&ante=morbi&ipsum=sem", "\"https://t.co/amet/cursus/id/turpis/integer/aliquet/massa.aspx?ligula=non&nec=mauris&sem=morbi&duis=non&aliquam=lectus&convallis=aliquam&nunc=sit&proin=amet&at=diam&turpis=in&a=magna&pede=bibendum&posuere=imperdiet&nonummy=nullam&integer=orci&non=pede&velit=venenatis&donec=non&diam=sodales&neque=sed&vestibulum=tincidunt&eget=eu&vulputate=felis&ut=fusce&ultrices=posuere&vel=felis&augue=sed&vestibulum=lacus&ante=morbi&ipsum=sem\"")]
        [InlineData("http://hao123.com/dictumst/aliquam/augue/quam.aspx?ac=augue&diam=quam&cras=sollicitudin&pellentesque=vitae&volutpat=consectetuer&dui=eget&maecenas=rutrum&tristique=at&est=lorem&et=integer&tempus=tincidunt&semper=ante&est=vel&quam=ipsum&pharetra=praesent&magna=blandit&ac=lacinia&consequat=erat&metus=vestibulum&sapien=sed&ut=magna&nunc=at&vestibulum=nunc&ante=commodo&ipsum=placerat&primis=praesent&in=blandit&faucibus=nam&orci=nulla&luctus=integer&et=pede&ultrices=justo&posuere=lacinia&cubilia=eget&curae=tincidunt&mauris=eget&viverra=tempus&diam=vel&vitae=pede&quam=morbi&suspendisse=porttitor&potenti=lorem&nullam=id&porttitor=ligula&lacus=suspendisse&at=ornare&turpis=consequat&donec=lectus&posuere=in&metus=est&vitae=risus&ipsum=auctor&aliquam=sed&non=tristique&mauris=in&morbi=tempus&non=sit&lectus=amet&aliquam=sem&sit=fusce&amet=consequat&diam=nulla&in=nisl&magna=nunc&bibendum=nisl&imperdiet=duis&nullam=bibendum&orci=felis&pede=sed&venenatis=interdum&non=venenatis&sodales=turpis&sed=enim&tincidunt=blandit&eu=mi&felis=in", "\"http://hao123.com/dictumst/aliquam/augue/quam.aspx?ac=augue&diam=quam&cras=sollicitudin&pellentesque=vitae&volutpat=consectetuer&dui=eget&maecenas=rutrum&tristique=at&est=lorem&et=integer&tempus=tincidunt&semper=ante&est=vel&quam=ipsum&pharetra=praesent&magna=blandit&ac=lacinia&consequat=erat&metus=vestibulum&sapien=sed&ut=magna&nunc=at&vestibulum=nunc&ante=commodo&ipsum=placerat&primis=praesent&in=blandit&faucibus=nam&orci=nulla&luctus=integer&et=pede&ultrices=justo&posuere=lacinia&cubilia=eget&curae=tincidunt&mauris=eget&viverra=tempus&diam=vel&vitae=pede&quam=morbi&suspendisse=porttitor&potenti=lorem&nullam=id&porttitor=ligula&lacus=suspendisse&at=ornare&turpis=consequat&donec=lectus&posuere=in&metus=est&vitae=risus&ipsum=auctor&aliquam=sed&non=tristique&mauris=in&morbi=tempus&non=sit&lectus=amet&aliquam=sem&sit=fusce&amet=consequat&diam=nulla&in=nisl&magna=nunc&bibendum=nisl&imperdiet=duis&nullam=bibendum&orci=felis&pede=sed&venenatis=interdum&non=venenatis&sodales=turpis&sed=enim&tincidunt=blandit&eu=mi&felis=in\"")]
        [InlineData("http://accuweather.com/auctor/gravida/sem.js?aliquam=aliquam&augue=convallis&quam=nunc&sollicitudin=proin&vitae=at&consectetuer=turpis&eget=a&rutrum=pede&at=posuere&lorem=nonummy&integer=integer&tincidunt=non&ante=velit&vel=donec&ipsum=diam&praesent=neque&blandit=vestibulum&lacinia=eget&erat=vulputate&vestibulum=ut&sed=ultrices&magna=vel&at=augue&nunc=vestibulum&commodo=ante&placerat=ipsum&praesent=primis&blandit=in&nam=faucibus&nulla=orci&integer=luctus&pede=et&justo=ultrices&lacinia=posuere&eget=cubilia&tincidunt=curae&eget=donec&tempus=pharetra&vel=magna&pede=vestibulum&morbi=aliquet&porttitor=ultrices&lorem=erat&id=tortor&ligula=sollicitudin&suspendisse=mi&ornare=sit&consequat=amet&lectus=lobortis&in=sapien&est=sapien&risus=non&auctor=mi&sed=integer&tristique=ac&in=neque&tempus=duis&sit=bibendum&amet=morbi&sem=non&fusce=quam&consequat=nec&nulla=dui&nisl=luctus&nunc=rutrum&nisl=nulla&duis=tellus&bibendum=in&felis=sagittis&sed=dui&interdum=vel&venenatis=nisl&turpis=duis&enim=ac&blandit=nibh&mi=fusce&in=lacus&porttitor=purus&pede=aliquet&justo=at&eu=feugiat&massa=non&donec=pretium&dapibus=quis&duis=lectus&at=suspendisse&velit=potenti&eu=in&est=eleifend&congue=quam", "\"http://accuweather.com/auctor/gravida/sem.js?aliquam=aliquam&augue=convallis&quam=nunc&sollicitudin=proin&vitae=at&consectetuer=turpis&eget=a&rutrum=pede&at=posuere&lorem=nonummy&integer=integer&tincidunt=non&ante=velit&vel=donec&ipsum=diam&praesent=neque&blandit=vestibulum&lacinia=eget&erat=vulputate&vestibulum=ut&sed=ultrices&magna=vel&at=augue&nunc=vestibulum&commodo=ante&placerat=ipsum&praesent=primis&blandit=in&nam=faucibus&nulla=orci&integer=luctus&pede=et&justo=ultrices&lacinia=posuere&eget=cubilia&tincidunt=curae&eget=donec&tempus=pharetra&vel=magna&pede=vestibulum&morbi=aliquet&porttitor=ultrices&lorem=erat&id=tortor&ligula=sollicitudin&suspendisse=mi&ornare=sit&consequat=amet&lectus=lobortis&in=sapien&est=sapien&risus=non&auctor=mi&sed=integer&tristique=ac&in=neque&tempus=duis&sit=bibendum&amet=morbi&sem=non&fusce=quam&consequat=nec&nulla=dui&nisl=luctus&nunc=rutrum&nisl=nulla&duis=tellus&bibendum=in&felis=sagittis&sed=dui&interdum=vel&venenatis=nisl&turpis=duis&enim=ac&blandit=nibh&mi=fusce&in=lacus&porttitor=purus&pede=aliquet&justo=at&eu=feugiat&massa=non&donec=pretium&dapibus=quis&duis=lectus&at=suspendisse&velit=potenti&eu=in&est=eleifend&congue=quam\"")]
        public void ReadJson_ValidInput(string value, string json)
        {
            var expected = new Uri(value);
            var converter = MaybeFalseUriConverter.Instance;
            using(var stringReader = new StringReader(json))
            using(var jsonReader = new JsonTextReader(stringReader))
            {
                var actual = converter.ReadJson(jsonReader, null, null, false, NewtonsoftJsonSerializer.Deserializer);
                Assert.Equal(expected, actual);
            }
        }

        [Theory]
        [InlineData("null")]
        [InlineData("false")]
        public void ReadJson_InvalidInputToNull(string json)
        {
            var converter = MaybeFalseUriConverter.Instance;
            using(var stringReader = new StringReader(json))
            using(var jsonReader = new JsonTextReader(stringReader))
            {
                var actual = converter.ReadJson(jsonReader, null, null, false, NewtonsoftJsonSerializer.Deserializer);
                Assert.Null(actual);
            }
        }

        [Fact]
        public void WriteJson_IsNotImplemented()
        {
            Assert.Throws<NotImplementedException>(() => MaybeFalseUriConverter.Instance.WriteJson(null, null, null));
        }
    }
}