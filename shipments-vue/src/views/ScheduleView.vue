<template class="schedule-ctnr">
    <Spinner v-if="isPending" />
    <div class="back-arrow">
        <span class="material-symbols-outlined" @click="goBack">
            arrow_circle_left
        </span>
    
    </div>    
    <div class="summary-ctnr">
        <div class="palletCount" >
            
            <h2 v-if="shipmentsCount == 0"><span class="dmonth">{{moment().format("DD-MMM")}}</span><span>{{moment().format("dddd")}}</span> Dzisiaj nie ma wysyłek</h2>
            <h2 v-if="shipmentsCount == 1"><span class="dmonth">{{moment().format("DD-MM")}}</span><span>{{moment().format("dddd")}}</span> Dzisiaj mamy <span>{{shipmentsCount}}</span> wysyłkę <span>{{palletCount}}</span> PAL</h2>
            <h2 v-if="shipmentsCount > 1 && shipmentsCount < 5"><span class="dmonth">{{moment().format("DD-MMM")}}</span><span>{{moment().format("dddd")}}</span> Dzisiaj mamy <span>{{shipmentsCount}}</span> wysyłki <span>{{palletCount}}</span> PAL</h2>
            <h2 v-if="shipmentsCount > 4"><span class="dmonth">{{moment().format("DD-MMM")}}</span><span>{{moment().format("dddd")}}</span> Dzisiaj mamy <span>{{shipmentsCount}}</span> wysyłek <span>{{palletCount}}</span> PAL</h2>
        </div>
        <div class="clock-ctnr">
            <h1>{{hours}}</h1>
            <div class="colon">:</div>
            <h1>{{minutes}}</h1>
            <div class="colon">:</div>
            <h2>{{sec}}</h2>
        </div>
        
    </div>
    <div class="navbar">
        <div class="navbar-line"></div>
        <div class="schedule-container schedule-bar">
            <div>
                <div class="schedule-header">
                    <p></p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>KLIENT</p>
                </div>
            </div>
            <div>
               <div class="schedule-header">
                    <p>MIEJSCE</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>ZAMÓWIENIE</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>LOKACJA</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>PAL #</p>
                </div>
            </div>
            
            <div>
                <div class="schedule-header">
                    <p>ETD</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>NR KONTENERA</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>NR REJESTRACJI</p>
                </div>
            </div>
            <div>
                <div class="schedule-header">
                    <p>STATUS</p>
                </div>
            </div>
            <!-- <div>
                <div class="schedule-header">
                    <p>NPIS</p>
                </div>
            </div> -->
        </div>
        <div class="navbar-line"></div>
    </div>



    <div class="frame">
        
        <div v-if="!error" >
            <div v-for="shipment in shipments" :key="shipment.id" class="schedule-container scheduleRow" 
                 @click="gotoShipment(shipment.id)"
                 :class="{
                    statusCompleated:shipment.status == 'Zrealizowana',
                    statusBlocked:shipment.status == 'Wstrzymana',
                    statusCanceled:shipment.status == 'Anulowana',
                    secondRow: shipment.counter % 2 == 0,
                    prioRow: shipment.hasPriority
                    }"
                    >
                <!-- npis -->
                <div v-if="shipment.hasPriority" class="npis">
                    <p>NPIS</p>
                </div>
                <div v-else>
                    <p></p>
                </div>
                <!-- klient -->
                <!-- :class="{doubleline:shipment.purchaseOrders.length>1} -->
                <div v-if="shipment.purchaseOrders.length != 0">
                    <div v-for="(order, index) in shipment.purchaseOrders" :key="order.id" class="clientName" :class="{doubleline:shipment.purchaseOrders.length>1}">
                        <span :class="{moreClients:shipment.purchaseOrders.length>1}">
                            <span class="index">{{index+1}}</span>
                            {{order.customerShortName}}
                        </span>
                    </div>
                    <!-- <div v-for="client in shipment.Clients" :key=client class="clientName" :class="{doubleline:shipment.Clients.length>1}">
                        <span :class="{moreClients:shipment.Clients.length>1}">
                            {{client}}
                        </span>
                    </div> -->
                </div>
                <div v-else>
                    <div>
                        N/A
                    </div>
                </div>
<!-- :class="{statusDelivered:delivery.statusId==3, statusInTransit:delivery.statusId==2, statusCreated:delivery.statusId==1}" -->
                <!-- miejsce -->
                <div v-if="shipment.purchaseOrders.length != 0">
                    <div v-for="order in shipment.purchaseOrders" :key="order.id" class="doubleline" >
                        {{order.customerCity}}
                    </div>
                </div>
                <div v-else>
                    <div>
                        N/A
                    </div>
                </div>
                <!-- zamówienie -->
                <div v-if="shipment.purchaseOrders.length != 0">
                    <div v-for="(order, index) in shipment.purchaseOrders" :key="order.id" class="doubleline"
                    :class="{
                        sample:order.category == 'Sample'}">
                         <span class="index">{{index+1}}</span>
                        {{order.category}}: {{order.poNumber}} 
                    </div>
                </div>
                <div v-else>
                    <div>
                        N/A
                    </div>
                </div>
                <!-- TPA -->
                <div v-if="shipment.warehouseArea">{{shipment.warehouseArea}}</div>
                <div v-else>N/A</div>
                <!-- PAL# -->
                <div v-if="shipment.palletQty">{{shipment.palletQty}}</div>
                <div v-else>N/A</div>
                <!-- ETD -->
                <div v-if="shipment.etd" :class="{statusDelayed: shipment.status != 'Zrealizowana'&& moment(shipment.etd) < moment()}">
                    <span v-if="shipment.status != 'Zrealizowana'">
                        {{moment(shipment.etd).format("DD-MMM-YY / HH:mm")}}
                    </span>
                    <span v-else>
                        {{moment(shipment.timeOfDeparture).format("DD-MMM-YY / HH:mm")}}
                    </span>
                </div>
                <div v-else>N/A</div>
                <!-- CTNR# -->
                <div v-if="shipment.containerType">{{shipment.containerType}}
                    <span v-if="shipment.containerNumber">#: {{shipment.containerNumber}}</span>
                </div>
                <!-- <div v-if="shipment.containerNumber">{{shipment.containerNumber}}</div> -->
                <div v-else>N/A</div>
                <!-- NR REJ. -->
                <div v-if="shipment.forwarder != null">{{shipment.forwarder.carPlates}}</div>
                <div v-else>N/A</div>
                <!-- STATUS -->
                <div class="status" v-if="shipment.status != null" 
                    :class="{
                        statusDone:shipment.status == 'Zrealizowana' || shipment.status == 'Gotowa',
                        statusBlocked:shipment.status == 'Wstrzymana',
                        statusCanceled:shipment.status == 'Anulowana'
                        }">
                    <span>
                        {{shipment.status}}
                    </span>
                    <!-- <span v-if="shipment.hasPriority" class="material-symbols-outlined timer">new_label</span> -->
                </div>
                <div v-else>N/A</div>
                

            </div>
        </div>

    </div>
</template>

<script>
import { onBeforeMount, onMounted, onUnmounted, ref } from 'vue'
import getScheduledShipments from '../js-components/getScheduledShipments.js'
import moment from 'moment'
import { useLinksStore } from '../stores/linksStore.js'
import {useRouter} from 'vue-router'
import Spinner from '../components/SpinnerComponent.vue'
export default {
    props:['userIsOffice'],
    components:{Spinner},
    setup(props){
        const router = useRouter()
        const linksStore = useLinksStore()
        const {loadShipments, error, shipments, isPending} = getScheduledShipments(linksStore.url)
        const palletCount = ref(0)
        const shipmentsCount = ref(0)

        onBeforeMount(()=>{
            loadScheduleShipments()
            
            document.body.classList.add("stop-scrolling");
        
        })
        
        const loadScheduleShipments = ()=>{
            loadShipments()
                .then(()=>{
                    shipmentsCount.value = 0
                    palletCount.value = 0
                    let counter = 0
                    shipments.value.forEach(shipment => {
                        shipment['counter']=counter++
                        if(moment(shipment.etd).format("YYY-MM-DD") == moment().format("YYY-MM-DD") || moment(shipment.timeOfDeparture).format("YYY-MM-DD") == moment().format("YYY-MM-DD")){
                            shipmentsCount.value++
                            palletCount.value = palletCount.value + shipment.palletQty
                        }
                    
                    });
            })
        }
       
    
          function requestFullScreen(element) {
              // Supports most browsers and their versions.
                var requestMethod = element.requestFullScreen || element.webkitRequestFullScreen || element.mozRequestFullScreen || element.msRequestFullScreen;

                if (requestMethod) { // Native full screen.
                    requestMethod.call(element);
                    } else if (typeof window.ActiveXObject !== "undefined") { // Older IE.
                        var wscript = new window.ActiveXObject("WScript.Shell");
                        if (wscript !== null) {
                            wscript.SendKeys("{F11}");
                        }
                    }
                }

        const refreshScreenOn = ()=>{
            var elem = document.getElementById("app") // Make the body go full screen.
            requestFullScreen(elem);
            setInterval(()=>{
                loadScheduleShipments()
            }, 1000*120)
        }  
        
        const gotoShipment = (shipmentId) =>{
            
            if(props.userIsOffice == 'false'){
                console.log('warehouse')
                router.push({ name:'SingleShipmentWarehouseView', 
                          params:{ shipmentId:shipmentId }
                          }) 
            }
            if(props.userIsOffice == 'true'){
                console.log('office')
                router.push({ name:'SingleShipmentView', 
                            params:{ shipmentId:shipmentId }
                            }) 
            }
            
        }

        const goBack = () =>{
            router.go(-1);
            document.body.classList.remove("stop-scrolling");
        }
           
        const myInterval = setInterval(()=>{
                loadScheduleShipments()
                }, 1000*120)
        const clockInterval = setInterval(()=>{
                startTime()
                }, 1000)
        onMounted(()=>{
            myInterval
            startTime()
            clockInterval
        })
     
        onUnmounted(()=>{
            document.body.classList.remove("stop-scrolling")
            clearInterval(myInterval)
            clearInterval(clockInterval)
        })

        //clock

        const hours = ref()
        const minutes = ref()
        const sec = ref()


        function startTime() {
            const today = new Date()
            hours.value = today.getHours()
            minutes.value = today.getMinutes()
            sec.value = today.getSeconds()
            minutes.value = checkTime(minutes.value)
            sec.value = checkTime(sec.value)
        }

        function checkTime(i) {
            if (i < 10) {i = "0" + i}  // add zero in front of numbers < 10
            return i
        }

        return{
            isPending,
             error, 
             shipments, 
             moment, 
             refreshScreenOn, gotoShipment,
             palletCount, shipmentsCount, 
             goBack,
             hours, minutes, sec
        }
    }

}
</script>

<style>
.stop-scrolling{
    overflow: hidden !important;
}
.summary-ctnr {
    position: absolute;
    height: 9vh;
    top: 0;
    right: 0;
    display: flex;
    gap: 5vw;
    
}
.clock-ctnr{
    
    padding: 0 1vw;
    margin: 0;
    display: flex;
    gap: 0.2vw;
    
}
.clock-ctnr h1{
    text-align: right;
    margin: auto 0;
    padding: 0;
    z-index: 101;
    font-size: 6.5vh;
    
}
.clock-ctnr .colon{
    padding: 0 0 0.8vh 0;
    color: #fdc700;
    font-size: 3vh;
    align-self: center;
}
.clock-ctnr h2{
    margin: auto 0;
    padding: 0;
    color: #fdc700;
    font-size: 3.2vh;
    
}
.palletCount{
    
    text-align: right;
    padding: 0;
    margin: 0%;
    z-index: 100;
    align-self: center;
}
.palletCount h2{
    padding: 0;
    margin: 0%;
    z-index: 100;
    color:#ddd;
    font-size: 2.5vh;
}
.palletCount h2 span{
    font-size: 3.8vh;
    color: #fdc700;
    margin:0 0.5vh;
}
.palletCount h2 span.dmonth{
    color: #ddd;
}
.back-arrow {
    position: fixed;
    padding: 0;
    margin: 0%;
    top: 11.0vh;
    left: 1vh;
    z-index: 100;
    height: 2vh;
    color:#ccc;
    font-size: 1.5vh;
    cursor: pointer;
}

.scheduleRow{

    background: linear-gradient(to right, #232938, #213D67);
    border-bottom: solid 0.1vh #ddd;
    
    /* border-color: #000000; */
    min-height: 7.0vh;
    box-shadow: inset 0 1vh 2vh #00000030;
    color: #ddd;
    cursor: pointer;
    overflow: hidden;
}
.secondRow{
    background: linear-gradient(to right, #252d4e, #1d488a);
    }
.prioRow{
    /* background: linear-gradient(to right, #38254e, #7e22af); */
}
.status{
    display: flex;
    justify-content: space-between;
    align-items: center;
}
.status .timer{
    font-size: 3vh ;
    margin: 0 0 0 1vh;
    padding: 0;
    color: #ffea00;

}
.schedule-container{
    font-family: 'Bebas Neue';
    
    font-weight: 200;
    font-size: 2.6vh;
    display: grid;
    align-items: center;
    grid-template-columns: 0.8fr 1.3fr 2.6fr 2.8fr 1.1fr 0.8fr 2.3fr 2.0fr 2.8fr 2fr;
    column-gap: 1.3vw;
    padding: 0.3vh 1.5vw 0.3vh 0.0vw;
    overflow:hidden;
}
.schedule-container div{
    word-wrap: none;
}
.schedule-container .npis{
    /* align-self: flex-start; */
}
.npis{
    position: relative;
    display: flex;
    left: 0;
    margin: 0;
    padding: 0;
    border: 0;
    background: linear-gradient( to right,#b79c00,#ffd900);
    align-items: center;
    justify-content: center;
    height: 2.9vh;
    width: 3.4vw;
    
    box-shadow: 0.3vh 0.3vh 0.4vh #00000099;
    border-right: 0.15vh solid #000;
    border-bottom: 0.15vh solid #000;
    /* box-shadow: inset 0 1vh 2vh #00000030; */
    border-radius: 0 2vh 2vh 0;
    
}
.npis p{
    padding: 0;
    margin: 0;
    font-size: 1.5vh;
    /* font-family:'Franklin Gothic Medium', 'Arial Narrow', Arial, sans-serif; */
    color: #171d39;
    transform: translateX(0.9vh) translateY(0.1vh);
}
.schedule-header p{
    font-family: 'Bebas Neue';
    font-weight: 500;
    font-size: 2.8vh;
}
.schedule-bar{
    height: 6.0vh;
    align-content: center
}

.schedule-container .doubleline{
    font-size: 2.0vh;
}
.statusDone{
    color: #b0f25a;
}
.statusBlocked{
    /* color: #f86262; */
    color: #ffa1a1;
    color: #ff874f;
}
.statusCompleated{
    color: #b0f25a;
}
.statusCanceled{
    color: #7b7b7b;
}
.statusDelayed{
    color: #ffb53f;
}
.clientName{
    color: #fdc700;
    font-size: 2.6vh;
}
.moreClients{
    font-size: 2.1vh;
}
.index{
    color: #777;
    font-size: 1.3vh;
    /* vertical-align: middle; */
}
.sample{
    color: #a8cbff;
}
</style>