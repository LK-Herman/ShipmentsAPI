<template>
  
  <div class="frame home-screen back02">

    <div class="nav-container">
        <div class="top-bar home-bar">
            <router-link :to="{name:'home'}">
                <img id="dsse-logo" src="@/assets/img/logoDSSE.png" alt="">
            </router-link>
            <div id="label">FG SHIPMENTS</div>
        </div>
        <div>
            <RouterView />
        </div>
    </div>
    <div class="img-ctnr">
        <div></div>
        <router-link :to="{name:'ShipmentsView'}" class="test-image first">
            <img src="../assets/img/biuro.jpg">
            <div>
                <h1>Biuro</h1>
                <h3>Awizacja wysyłek</h3>
            </div>
        </router-link>
        <router-link :to="{name:'ShipmentsWarehouseView'}" class="test-image first">
            <img src="../assets/img/magazyn.jpg">
            <div>
                <h1>Magazyn</h1>
                <h3>Obsługa wysyłek</h3>
            </div>
        </router-link>
        <!-- <router-link :to="{name:'ScheduleView', params:{userIsOffice:false}}" class="test-image first">
            <img src="../assets/img/harmonogram.jpg">
            <div>
                <h1>Wysyłki</h1>
                <h3>Harmonogram</h3>
            </div>
        </router-link> -->
        <div></div>
    </div>
     <div class="home-desc">
       <h1>Witaj Logistyku</h1>
       <p>Platforma <span>FG Shipments</span> została zbudowana w celu usprawnienia procesu komunikacji związanego 
       z wysyłkami wyrobów gotowych. Umożliwia tworzenie awizacji zawierających dane o zamówieniach, 
       klientach oraz przewoźnikach. Narzędzie pozwala na wymianę danych między logistykami (biuro 
       <span class="material-symbols-outlined desc-symbol">swap_horiz</span> magazyn), czego wynikiem jest czytelny harmonogram wysyłek w postaci tablicy informacyjnej (dashboard'u).</p>
    </div>
    <div class="ver">
        <p>
            UPDATED: 2026-05-19
        </p>
    </div>

  </div>
</template>

<script>
import moment from 'moment'
export default {
   
    setup(){
        
        return{moment}
    }
}
</script>
<style>
.home-screen{
 position: absolute;
    width: 100%;
    height: 100%;
    background: linear-gradient(to right, #202635,#303552, #202635);
    top: 0;
    z-index: 1000;
    transition: 200ms ease-in;
}
.home-screen.back01{
    background-image: linear-gradient(to right, #202635,#303552, #202635), url('../assets/img/back01.jpg');
    background-blend-mode: multiply;
    background-size: cover;
}
.home-screen.back02{
    background-image: linear-gradient(to right, #202635,#303552, #202635), url('../assets/img/back02.jpg');
    background-blend-mode: multiply;
    background-size: cover;
}
.home-desc{
    display: flex;
    flex-direction: column;
    justify-content: center;
    align-items: center;
    
    margin: 0vh auto;
}
.ver p{
    text-align: center;
    font-size: 1.2vh;
    font-weight: 600;
    color: #4a4a4a;
    margin-top: 4vh ;
    background: #15151560;
    padding: 1.5vh 0;
}
.home-desc span{
    font-weight: 400;
}
.home-desc .desc-symbol{
    
    margin:0;
    padding: 0;
    font-size: 1.8vh;
    vertical-align: sub;
}
.home-desc p{
    margin: 0.4vh;
    font-weight: 200;
    width: 84vh;
    text-align: justify;
}
#app .main-container .top-bar.home-bar{
    margin-top: 10vh;
    padding: 2vh 0;
    background: transparent;
    background:linear-gradient(to right, #00000070, #00000030, #ffffff00,#00000030, #00000070);
    border:solid 2px;
    border-left: 0px;
    border-right: 0px;
    justify-content: center;
    /* transform: translateY(5vh); */
    
}

.img-ctnr{
    display: flex;
    justify-content: center;
    align-items: center;
    height: 48vh;
    
}
.img-ctnr img{
    height: inherit;
    overflow: hidden;
    
}
.test-image{
    display: grid;
    grid-template-columns: auto auto;
    gap: 3vh;
    width: 45vh;
    height: 24vh;
    border-radius: 0vh;
    margin: 10vh 2vh;
    border: solid 2px #ddd;
    justify-content: start;
    align-items: center;
    background: linear-gradient(rgb(56, 56, 56), rgb(38, 41, 42));
}

.test-image h1{
    margin:0vh 0vh;
    font-family: 'Bebas Neue', cursive;
    background: -webkit-linear-gradient(#ffcc00, #fada27);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    font-size: 6.0vh;
    align-self: center;
    padding: 0;
    z-index: 2;
}
.test-image h3{
    margin:0vh 0vh;
    font-family: 'Bebas Neue', cursive;
    background: -webkit-linear-gradient(#ffffff, #e6e6e6);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    font-size: 3.0vh;
    align-self: center;
    padding: 0;
}
.test-image.first:hover{
    transform:scale(1.05);
    z-index: 2;
    transition: ease-in-out 200ms;
    box-shadow: 1.6vh 1.6vh 1.8vh #00000060;
    cursor: pointer;
}
.test-image.first{
    transition: ease-in-out 200ms;
}
.test-image.second:hover{
    transform:scale(2.0);
    z-index: 2;
    transition: ease-in-out 200ms;
}
.test-image.second{
    transform: rotateZ(-10deg) translateX(40px);
    transition: ease-in-out 300ms;
}
.test-image.third:hover{
    transform:scale(2.0);
    z-index: 2;
    transition: ease-in-out 300ms;
}
.test-image.third{
    transform: rotateZ(-26deg) translateX(20px);
    transition: ease-in-out 300ms;
}
</style>