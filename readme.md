# Dynamon kuumalinja

Suunnitelma ja loppuraportti löytyy repositorion wikistä.

## Asennus omalle palvelimelle


Tietokannan luontiscripti ja testidata löytyy repositorion Suunnittelu/Database -kansiosta.
<br/>
Avaa app.config tiedosto ja muokkaa sieltä  kohdan Connect arvoa vastaavaksi<br/> `Data source=palvelin;Initial Catalog=tietokanta;user=käyttäjä;password={0}` <br/> muokkaa myös Passwordin arvo tietokannan käyttäjän salasanaksi. 