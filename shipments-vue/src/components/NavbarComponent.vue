<template>
  <div class="navbar">
        <div class="navbar-line"></div>
        <div class="navbar-content">
            <div class="nav-left">
                <div>
                    <router-link :to="{name: 'ShipmentsView'}">
                        <p>WYSYŁKI</p>
                    </router-link>
                </div>
                <div>
                    <router-link :to="{name:'ScheduleView', params:{userIsOffice:true}}">
                        <p>HARMONOGRAM</p>
                    </router-link>
                </div>
                <div>
                    <router-link :to="{name: 'AddAllView'}">
                        <p id="addshipnav">+ <span class="material-symbols-outlined" >rocket_launch</span></p>
                    </router-link>
                </div>

            </div>
            <div class="nav-right">
                <div>
                    <router-link :to="{name:'CustomerView'}">
                        <p>KLIENCI</p>
                    </router-link>
                </div>
                <div>
                    <router-link :to="{name: 'PurchaseOrderView'}">
                        <p>ZAMÓWIENIA</p>
                    </router-link>
                </div>
                <div>
                        <router-link :to="{name:'ForwarderView'}">
                        <p>PRZEWOŹNICY</p>
                    </router-link>
                </div>

            </div>
             
        </div>
        <div class="navbar-line"></div>
    </div>
</template>

<script>
export default {
    // setup(){
    //     const reloadPage = ()=>{
    //         location.reload()
    //     }
    //     return { reloadPage}
    // }

}
</script>



<style scoped>
.material-symbols-outlined{
    font-size: 2.2vh;
    padding: 0;
    margin: 0;
}

#addshipnav:hover{
    transform: scale(1.4);
}



</style>