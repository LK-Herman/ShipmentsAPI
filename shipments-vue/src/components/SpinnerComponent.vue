<template>
    <div class="spiner-ctnr">
        <div class="spin"></div>
        <!-- <div class="spin-label"><p>to potrwa tylko chwilę ...  </p></div> -->
    </div>
    <div class="spinner-logo">
        <div class="spinner-img-ctnr">
            <!-- <img class="spinner-img" src="../assets/img/logoDSSE.png" alt="" srcset=""> -->
            <div class="spinner-img">
                <p>coś tam wczytuje..</p>
                <p>. . . </p>
                <p>niech poczeka.</p>
            </div>
        </div>
    </div>
</template>

<script>
export default {

}
</script>

<style>
.spiner-ctnr{
    position: absolute;
    width: 100%;
    height: 100%;
    background: linear-gradient(to right, #000000b0,#00000060,#000000b0);
    top: 0;
    left: 0;
    border-radius: 0;
    z-index: 1000;
    transition: 300ms ease-in;
}
.spin{
    display: block;
    position: relative;
    top: 0;
    left: 0;
    box-shadow: 0.5vh 0.5vh #ffffffd0;
    
    width: 13vh;
    height: 13vh;
    margin:31.5vh auto 2vh auto;
    border: 0.9vh solid #08213fd9;
    border: 0.4vh solid transparent;
    border-radius: 50%;
    /* border-top-color: #ff8800; */
    animation: spin 1s linear infinite;
}
@keyframes spin {
    to { transform: rotateZ(360deg)}
}
.spin-label p{
    margin: 2px auto;
    text-align: center;
    color: #f7c600;
}
.spinner-logo{
    position: absolute;
    width: 100%;
    height: 100%;
    background: transparent;;
    top: 0;
    left: 0;
    border-radius: 0;
    z-index: 1001;
    transition: 300ms ease-in;
}
.spinner-img-ctnr{
    display: block;
    width: 15vh;
    height: 15vh;
    /* background: #2458a2; */
    box-shadow:inset 0vh 0vh 4vh #0d2f5ad9;
    border-radius: 50%;
    margin: 30.85vh auto 0vh auto;
    background: #10407bd9;
    
    transform: scale(0.9) rotateZ(-8deg);
    
    z-index: 999;
}
.spinner-img{
    display: block;
    position: relative;
    top: 2vh;
    margin: auto ;
    text-align: center;
    vertical-align: middle;
    
}
.spinner-img p{
    font-size: 1.3vh;
    margin: 0.5vh 2.8vh;
    
}
</style>