<template>
    <NavbarComponent />
   <div class="frame">
     <div class="view-container">
        <div class="sub-page-header item-e ">
            <p>WYSYŁKI</p>
        </div>
        <div class="item-a" >
            <div class="item-container main-add-btn" @click="openAddComponent">
                <p>
                    <span class="material-symbols-outlined">add</span>
                    <span class="material-symbols-outlined">rocket_launch</span>
                </p>

            </div>
        </div>
        <div class="item-v">
            <ShipmentsList :userIsOffice="true"/>
        </div>
  </div>
  </div>
</template>

<script>

import NavbarComponent from '../components/NavbarComponent.vue'
import ShipmentsList from "../components/ShipmentsList.vue"
import { useRouter } from 'vue-router'
export default {
    components: {ShipmentsList, NavbarComponent},
    setup(){
        const router = useRouter();
        
        const openAddComponent = ()=>{
            router.push({ name:'AddAllView' })
        }
        return{ openAddComponent }                          
                
    }

}
</script>

<style>

</style>