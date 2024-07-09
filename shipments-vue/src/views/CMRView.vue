<template>
    <NavbarWarehouse />
        <form class="cmr-form" @submit.prevent="">
            
            <div class="cmr-container">
            <!-- 1 column -->
            <div>
                <span class="cmr-client-header">
                    <h3 class="top-headers">Klient</h3> <h3 class="top-headers yell">{{formConsigneeName}}</h3>
                </span>
                
               
                    <p class="choose-clinet-head" :class="{greenLabel:formClientIndex == null}">Wybierz klienta</p>
                
                    <div class="clients-icons-ctnr">
                        <div 
                            class="client-icon" 
                            v-for="(client, index) in clients" :key="client.id" 
                            :value="index" 
                            @click="chooseClient(index)" 
                            :class="{notChosenIcon:formClientIndex != index}"
                            >
                            <p>
                                {{client.shortName}}
                            </p>
                        </div>
                    </div>
    <!-- <span v-if="formClientIndex != null"> -->
                <h3>1 - Nadawca</h3>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formSenderName == ''}">Nazwa firmy</label>
                    <input class="dis" type="text" v-model="formSenderName">
                </div>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formSenderStreet == ''}">Ulica</label>
                    <input class="dis" type="text" v-model="formSenderStreet">
                </div>
                    
                <div class="cmr-form-set">
                    <label :class="{alertfont:formSenderCity == ''}">Kod pocztowy i miasto</label>
                    <input class="dis" type="text" v-model="formSenderCity">
                </div>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formSenderCountry == ''}">Kraj</label>
                    <input class="dis" type="text" v-model="formSenderCountry">
                </div>
                <h3>2 - Odbiorca</h3>
                <div class="cmr-form-set">
                    <label>Nazwa firmy</label>
                    <input class="dis" type="text" v-model="formConsigneeName">
                </div>
                <div class="cmr-form-set">
                    <label>Ulica</label>
                    <input class="dis" type="text" v-model="formConsigneeStreet">
                </div>
                    
                <div class="cmr-form-set">
                    <label>Kod pocztowy i miasto</label>
                    <input class="dis" type="text" v-model="formConsigneeCity">
                </div>
                <div class="cmr-form-set">
                    <label>Kraj</label>
                    <input class="dis" type="text" v-model="formConsigneeCountry">
                </div>
                <h3>3 - Miejsce przeznaczenia</h3>
                <div class="cmr-form-set">
                    <label>Miejscowość, kraj</label>
                    <input class="dis" type="text" v-model="formDestination">
                </div>
                <h3>4 - Miejsce i data załadowania</h3>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formLoadingPlace == ''}">Miejscowość, kraj, data</label>
                    <input class="dis" type="text" v-model="formLoadingPlace">
                </div>
                <h3>5 - Załączone dokumenty</h3>
                <div class="cmr-form-set">
                    <label >Numer dokumentu 1</label>
                    <input class="dis" type="text" v-model="formAttachment1">
                </div>
                <div class="cmr-form-set">
                    <label>Numer dokumentu 2</label>
                    <input class="dis" type="text" v-model="formAttachment2">
                </div>
<!-- </span> -->
            </div>    
            
            <!-- 2 column -->
            <div>
                <h3>6 - Cechy i numery towaru</h3>
                <div class="cmr-form-set">
                    <label  :class="{alertfont:formGoodsMarks1 == ''}">Linia #1</label>
                    <input class="dis" type="text" v-model="formGoodsMarks1">
                </div>
                <div class="cmr-form-set">
                    <label  :class="{alertfont:formGoodsMarks2 == ''}">Linia #2</label>
                    <input class="dis" type="text" v-model="formGoodsMarks2">
                </div>
                <div class="cmr-form-set">
                    <div class="linecheck">
                        <label>Linia #3</label>
                        <input class="dis" type="checkbox" v-model="isLine3active" >
                    </div>
                    <input id="inputLine3" type="text" v-model="formGoodsMarks3">
                </div>
                <div class="cmr-form-set">
                    <div class="linecheck">
                        <label>Linia #4</label>
                        <input class="dis" type="checkbox" v-model="isLine4active">
                    </div>
                    <input id="inputLine4" type="text" v-model="formGoodsMarks4">
                </div>
                <div class="cmr-form-set">
                    <div class="linecheck">
                        <label>Linia #5</label>
                        <input class="dis" type="checkbox" v-model="isLine5active">
                    </div>
                    
                    <input id="inputLine5" type="text" v-model="formGoodsMarks5">
                </div>

                <div class="cmrquest">
                    <div class="cmrquest-item">
                        <input class="dis" type="checkbox" v-model="isDgd">
                        <label @click="isDgd=!isDgd">UN 3268</label>
                    </div>
                    <div class="cmrquest-item">
                        <input class="dis" type="checkbox" v-model="isAdrRegulated">
                        <label @click="isAdrRegulated = !isAdrRegulated">Klauzula ADR: Przewóz zgodny z 1.1.4.2.1 </label>
                    </div>
                    <div class="cmrquest-item">
                        <input class="dis" type="checkbox" v-model="isOverpack">
                        <label @click="isOverpack = !isOverpack">Zastosowano opakowanie zbiorcze OVERPACK.</label>
                    </div>
                </div>

                    <h3>7 - Ilość</h3>
                    <div class="cmr-form-set">
                        <label :class="{alertfont:formGoodsQty == ''}">Iość palet (szt)</label>
                        <input class="dis" type="text" v-model="formGoodsQty">
                    </div>
                
                <h3>11 - Waga</h3>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formGoodsNet == ''}">Waga netto towaru(kg)</label>
                    <input class="dis" type="text" v-model="formGoodsNet">
                </div>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formGoodsWeight == ''}">Waga całkowita brutto (kg)</label>
                    <input class="dis" type="text" v-model="formGoodsWeight">
                </div>
                <h3>12 - Objętość</h3>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formGoodsCBM == ''}">Objętość (m<sup>3</sup>)</label>
                    <input class="dis" type="text" v-model="formGoodsCBM">
                </div>
                <div class="cmr-form-set cbm-btn">
                    <label>Kalkulator objętości</label>
                    <div class="btn  dis" :class="{notChosenCalc:formClientIndex == null}" @click="handleShowCalc">
                        <span class="material-symbols-outlined">calculate</span>
                    </div>
                </div>
                <h3>16 - Przewoźnik</h3>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formSpedName == ''}">Imię i nazwisko</label>
                    <input class="dis" type="text" v-model="formSpedName">
                </div>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formSpedCompany == ''}">Firma</label>
                    <input class="dis" type="text" v-model="formSpedCompany">
                </div>
                <div class="cmr-form-set">
                    <label :class="{alertfont:formSpedCarPlates == ''}">Numery rejestracyjne</label>
                    <input class="dis" type="text" v-model="formSpedCarPlates">
                </div>
                <h3></h3>
                <div v-if="formClientIndex != null" class="double-btns">
                    <button class="btn btn-cmr" @click="showPdf()"> Pokaż CMR</button>
                    <button class="btn btn-cmr" @click="downloadPdf()">Pobierz CMR </button>
                    <button class="btn btn-cmr reset" @click="resetForm()">Anuluj</button>
                </div>
                <div v-else class="no-btns">
                    <p>Wybierz klienta, dla którego chcesz wypełnić list przewozowy.</p>
                </div>
                
            </div>
            
           
            </div>
        </form>
    
    <!-- <div >
        <iframe id="pdf" title="CMR" name="CMR"> </iframe>
    </div> -->
    
    <div v-if="showCalcFlag">
        <CBMcalculator @calculateCbmEvent="handleCalculateCbm"/>
    </div>
  
</template>

<script>
import { onMounted, onUnmounted, ref, watch, watchEffect } from 'vue'
import getShipmentById from '../js-components/getShipmentById'
import getAllCustomers from '../js-components/getAllCustomers'
import { useLinksStore } from '../stores/linksStore.js'
import moment from 'moment/dist/moment'
import { PDFDocument, rgb, StandardFonts } from 'pdf-lib';
import download from 'downloadjs'
import NavbarWarehouse from '../components/NavbarWarehouse.vue'
import CBMcalculator from '../components/CBMcalculator.vue'
// import cmrfile from '../assets/documents/cmr.pdf'

export default {
    components:{NavbarWarehouse, CBMcalculator},
    props:['shipmentId'],
    setup(props){
        const linksStore = useLinksStore()
        const clients = ref([])
        const { loadShipment, error, shipment, isPending} = getShipmentById(linksStore.url)
        const { loadAllCustomers, error:customersError, customers} = getAllCustomers(linksStore.url)
        let singleCustomer
        const isShown = ref(false)

        const formClientIndex = ref()
        const showCalcFlag = ref(false)

        // 1 - Sender
        const formSenderName = ref('Daicel Safety Systems Europe Sp z o.o.')
        const formSenderStreet = ref('ul. Sterefowa 6')
        const formSenderCity = ref('58-130 Żarów')
        const formSenderCountry = ref('PL')
        // 2 - Consignee
        const formConsigneeName = ref('')
        const formConsigneeStreet = ref('')
        const formConsigneeCity = ref('')
        const formConsigneeCountry = ref('')
        
        const formDestination = ref('')
        const formLoadingPlace = ref('')
        const formAttachment1 = ref('Instructions in writing according to ADR, DGD, SDS')
        const formAttachment2 = ref('Delivery Note: ')
        //6 Goods
        const formGoodsMarks1 = ref('UN 3268 SAFETY DEVICES, 9, (E)')
        const formGoodsMarks2 = ref('UN 3268 URZĄDZENIA BEZPIECZEŃSTWA, 9, (E)')
        const formGoodsMarks3 = ref('1000 Fiberboard boxes (4G)')
        const formGoodsMarks4 = ref('1000 Skrzyn tekturowych (4G)')
        const formGoodsMarks5 = ref('')
        const isDgd = ref(true)
        const isAdrRegulated = ref(true)
        const isOverpack = ref(true)
        const isLine3active = ref(false)
        const isLine4active = ref(false)
        const isLine5active = ref(false)

        const formGoodsWeight = ref('')
        const formGoodsNet = ref('')
        
        const formGoodsCBM = ref('')
        const formGoodsQty = ref('')
        
        //16 Speditor
        const formSpedCompany = ref('')
        const formSpedName = ref('')
        const formSpedCarPlates = ref('')

        function replace(sentence) {
            let sentenceCharArray = sentence.split("")
            sentenceCharArray.forEach((char,index) => {
                switch (char) {
                    case 'ą':
                        sentenceCharArray[index] = 'a'
                        break;
                    case 'Ą':
                        sentenceCharArray[index] = 'A'
                        break;
                    case 'ć':
                        sentenceCharArray[index] = 'c'
                        break;
                    case 'Ć':
                        sentenceCharArray[index] = 'C'
                        break;
                    case 'ę':
                        sentenceCharArray[index] = 'e'
                        break;
                    case 'Ę':
                        sentenceCharArray[index] = 'E'
                        break;
                    case 'ł':
                        sentenceCharArray[index] = 'l'
                        break;
                    case 'Ł':
                        sentenceCharArray[index] = 'L'
                        break;
                    case 'ń':
                        sentenceCharArray[index] = 'n'
                        break;
                    case 'Ń':
                        sentenceCharArray[index] = 'N'
                        break;
                    case 'ó':
                        sentenceCharArray[index] = 'o'
                        break;
                    case 'Ó':
                        sentenceCharArray[index] = 'O'
                        break;
                    case 'ś':
                        sentenceCharArray[index] = 'ś'
                        break;
                    case 'Ś':
                        sentenceCharArray[index] = 'S'
                        break;
                    case 'ź':
                        sentenceCharArray[index] = 'z'
                        break;
                    case 'Ź':
                        sentenceCharArray[index] = 'Z'
                        break;
                    case 'ż':
                        sentenceCharArray[index] = 'z'
                        break;
                    case 'Ż':
                        sentenceCharArray[index] = 'Z'
                        break;
                
                    default:
                        break;
                }
            });
            const newSentence = sentenceCharArray.join("");
            return newSentence;
        }
        const handleShowCalc = () =>{
            showCalcFlag.value = true
        }
       

        const getData = ()=>{
             loadShipment(props.shipmentId).then(()=>{
                shipment.value.forwarder.firstName = replace(shipment.value.forwarder.firstName)
                shipment.value.forwarder.lastName = replace(shipment.value.forwarder.lastName)
                shipment.value.forwarder.speditor = replace(shipment.value.forwarder.speditor)

                loadAllCustomers().then(()=>{
                    shipment.value.purchaseOrders.forEach(order => {
                        singleCustomer = customers.value.filter(customer => customer.shortName == order.customerShortName)
                        singleCustomer = singleCustomer.map((customer)=>{
                            return {
                                        id:customer.id, 
                                        name:replace(customer.name),
                                        shortName: replace(customer.shortName),
                                        streetAddress: replace(customer.streetAddress),
                                        cityAddress: replace(customer.cityAddress),
                                        zipCodeAddress: replace(customer.zipCodeAddress),
                                        countryAddress: replace(customer.countryAddress)
                                }
                        })
                        clients.value.push(singleCustomer[0])
                    })
                    var tempArray = clients.value.sort((a, b) => {
                    const nameA = a.id; 
                    const nameB = b.id; 
                    if (nameA < nameB) {
                        return -1;
                    }
                    if (nameA > nameB) {
                        return 1;
                    }
                    // names must be equal
                    return 0;
                    });
                   
                    
                    tempArray = tempArray.filter((value, index, array)=>{
                        if (index > 0){
                            return array[index].shortName !== array[index-1].shortName
                        }else{
                            return array[index]
                        }
                    })
                    
                    clients.value = tempArray

                });
            })
        }
        // const pdfUrl = '/src/assets/documents/cmr.pdf'
        const pdfUrl = '/cmr.pdf'
        let outputUint8Array = null
        onMounted(()=>{
            getData()
            toggleDisableInputs(true)
            toggleDisableInputs345(true)
        })

        const toggleDisableInputs345 = (flag)=>{
            isLine3active.value = !flag
            showDomElementById('inputLine3', flag)
            isLine4active.value = !flag
            showDomElementById('inputLine4', flag)
            isLine5active.value = !flag
            showDomElementById('inputLine5', flag)
        }
        
        async function modifyPdf(pdfUrl) {
            
            const pngUrl = '/src/assets/img/logoDSSEblue.png'
            const pngImageBytes = await fetch(pngUrl).then((res) => res.arrayBuffer())
            const pictogramUrl = '/src/assets/img/ADR9.png'
            const pictogramImageBytes = await fetch(pictogramUrl).then((res) => res.arrayBuffer())

            const existingPdfBytes = await fetch(pdfUrl).then(res => res.arrayBuffer())
            const pdfDoc = await PDFDocument.load(existingPdfBytes)
            const helveticaFont = await pdfDoc.embedFont(StandardFonts.Helvetica)
            const pages = pdfDoc.getPages()
            
            const pngImage = await pdfDoc.embedPng(pngImageBytes)
            const pngPictogram = await pdfDoc.embedPng(pictogramImageBytes)

            fillEachPdfPage(pages, helveticaFont, pngImage, pngPictogram)
            const pdfBytes = await pdfDoc.save().then(res => outputUint8Array = res)
            return pdfBytes
        }
        const fillEachPdfPage = (pages, font, pngImage)=>{
            const pngDims = pngImage.scale(0.25)
            // const pictogramDims = pngPictogram.scale(0.09)
            
            
            pages.forEach(page => {
                page.drawImage(pngImage, {
                    x: page.getWidth() / 2 - pngDims.width / 2 + 250,
                    y: page.getHeight() / 2 - pngDims.height + 410,
                    width: pngDims.width,
                    height: pngDims.height,
                })
                //  page.drawImage(pngPictogram, {
                //     x: page.getWidth() / 2 - pictogramDims.width / 2 - 224,
                //     y: page.getHeight() / 2 - pictogramDims.height + 3,
                //     width: pictogramDims.width,
                //     height: pictogramDims.height,
                // })
                const {  height } = page.getSize()
                const x = 50;
                const y = height;
                //1 Sender
                page.drawText(replace(formSenderName.value), {
                    x: x,
                    y: y - 70,
                    size: 9,
                    font: font,
                    color: rgb(0,0,0),
                    
                    })
                page.moveTo(x, y - 80)
                page.drawText(replace(formSenderStreet.value), { size: 9 })
                page.moveTo(x, y - 90)
                page.drawText(replace(formSenderCity.value), { size: 9 })
                page.moveTo(x, y - 100)
                page.drawText(replace(formSenderCountry.value), { size: 9 })
    
                //2 Consignee
                page.moveTo(x, y - 140)
                page.drawText(replace(formConsigneeName.value), { size: 9 })
                page.moveTo(x, y - 150)
                page.drawText(replace(formConsigneeStreet.value), { size: 9 })
                page.moveTo(x, y - 160)
                page.drawText(replace(formConsigneeCity.value), { size: 9 })
                page.moveTo(x, y - 170)
                page.drawText(replace(formConsigneeCountry.value), { size: 9 })
                //3 Destination
                page.moveTo(x, y - 220)
                page.drawText(replace(formDestination.value), { size: 9 })
                //4 Loading place
                page.moveTo(x, y - 264)
                page.drawText(replace(formLoadingPlace.value), { size: 9 })
                //5 Attachments
                page.moveTo(x, y - 298)
                page.drawText(replace(formAttachment1.value), { size: 9 })
                page.moveTo(x, y - 308)
                page.drawText(replace(formAttachment2.value), { size: 9 })
                //16 Speditor
                page.moveTo(x+280, y - 143)
                page.drawText(replace(formSpedCompany.value), { size: 9 })
                page.moveTo(x+280, y - 156)
                page.drawText(replace(formSpedName.value), { size: 9 })
                page.moveTo(x+280, y - 172)
                page.drawText(replace(formSpedCarPlates.value), { size: 12 })
                //18 Speditor comments
                page.moveTo(x+280, y - 260)
                page.drawText('', { size: 9 })
                page.moveTo(x+280, y - 270)
                page.drawText('', { size: 9 })
                //6 7 8 9 10 Marks and Nos
                page.moveTo(x , y - 350)
                page.drawText(replace(formGoodsMarks1.value), { size: 9 })
                page.moveTo(x , y - 360)
                page.drawText(replace(formGoodsMarks2.value), { size: 9, color: rgb(0.40, 0.40, 0.40) })
                page.moveTo(x, y - 375)
                page.drawText('Total quantity (Net. weight): '+ formGoodsNet.value.toString() + ' kg', { size: 9  })
                page.moveTo(x, y - 385)
                page.drawText('Ilosc calkowita (waga netto): '+ formGoodsNet.value.toString() + ' kg', { size: 9, color: rgb(0.40, 0.40, 0.40)  })
                if(isLine3active.value){
                    page.moveTo(x, y - 400)
                    page.drawText(replace(formGoodsMarks3.value), { size: 9 })
                }
                if(isLine4active.value){
                    page.moveTo(x, y - 410)
                    page.drawText(replace(formGoodsMarks4.value), { size: 9, color: rgb(0.40, 0.40, 0.40) })
                }
                if(isLine5active.value){
                    page.moveTo(x, y - 425)
                    page.drawText(replace(formGoodsMarks5.value), { size: 9  })
                }
                if(isOverpack.value){
                    page.moveTo(x, y - 440)
                    page.drawText('OVERPACK used: ' + formGoodsQty.value.toString()  + ' pallets', { size: 9 })
                    page.moveTo(x, y - 450)
                    page.drawText('Zastosowano opakowanie zbiorcze: ' + formGoodsQty.value.toString()  + ' palet', { size: 9, color: rgb(0.40, 0.40, 0.40) })
                }else{
                    page.moveTo(x + 210, y - 392)
                    page.drawText(formGoodsQty.value.toString()  + ' PALLETS', { size: 14 })
                    page.moveTo(x + 211 , y - 403)
                    page.drawText(formGoodsQty.value.toString()  + ' PALET', { size: 9, color: rgb(0.40, 0.40, 0.40) })
                }
                if(shipment.value.containerNumber.length > 3){
                    page.moveTo(x, y - 470)
                    page.drawText('CTNR#: '+ replace(shipment.value.containerNumber) + ' / ' + shipment.value.containerType, { size: 9 })
                }
                if(shipment.value.containerSealNumber.length > 3){
                    page.moveTo(x, y - 482)
                    page.drawText('SEAL#: '+ shipment.value.containerSealNumber, { size: 9 })
                }
                if(isDgd.value){
                    page.moveTo(x+20, y - 507)
                    page.drawText('9', { size: 9 })
                    page.moveTo(x+110, y - 507)
                    page.drawText('3268', { size: 9 })
                    page.moveTo(x+200, y - 507)
                    page.drawText('N/A', { size: 9 })
                }
                // 11 Weight
                page.moveTo(x+400, y - 350)
                page.drawText(formGoodsWeight.value.toString(), { size: 11 })
                //12 CBM
                page.moveTo(x+470, y - 350)
                page.drawText(formGoodsCBM.value.toString(), { size: 11 })
                //13 Senders instructions
                if(isAdrRegulated.value){
                    page.moveTo(x-3, y - 540)
                    page.drawText('Przewoz zgodny z 1.1.4.2.1', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                    page.moveTo(x-3, y - 550)
                    page.drawText('Carriage in accordance with 1.1.4.2.1', { size: 8 })
                }
                page.moveTo(x-3, y - 557)
                page.drawText('__________________________________________________', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x-3, y - 570)
                page.drawText('Ilosc calkowita dla 4 kategorii transportowej (1.1.3.6.3): ' + formGoodsNet.value.toString(), { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x-3, y - 580)
                page.drawText('Total quantity for transport category 4 (1.1.3.6.3): ' + formGoodsNet.value.toString(), { size: 8 })
                page.moveTo(x-3, y - 592)
                page.drawText('Wartosc obliczona dla 4 kategorii transportowej (1.1.3.6.4): 0', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x-3, y - 602)
                page.drawText('Value calculated for transport category 4 (1.1.3.6.4): 0', { size: 8 })
                page.moveTo(x-3, y - 614)
                page.drawText('Suma wartosci obliczonych dla wszystkich kategorii', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x-3, y - 624)
                page.drawText('transportowych (1.1.3.6.4): 0', { size: 8, color: rgb(0.36, 0.36, 0.36) })
                page.moveTo(x-3, y - 634)
                page.drawText('Total value calculeted for all transport categories: 0', { size: 8 })
                //21 Established in
                page.moveTo(x, y - 724)
                page.drawText(replace(formLoadingPlace.value), { size: 9 })
                //22 Stamp
                page.moveTo(x+10, y - 750)
                page.drawText('Daicel Safety Systems', { size: 11, color: rgb(0.60, 0.70, 0.90) })
                page.moveTo(x+27, y - 761)
                page.drawText('Europe Sp. z o.o.', { size: 11 , color: rgb(0.60, 0.70, 0.90) })
                page.moveTo(x+40, y - 772)
                page.drawText('ul. Strefowa 6', { size: 9 , color: rgb(0.60, 0.70, 0.90) })
                page.moveTo(x+39, y - 781)
                page.drawText('58-130 Zarow', { size: 9 , color: rgb(0.60, 0.70, 0.90) })
                page.moveTo(x+8, y - 790)
                page.drawText('NIP 886-27-53-552 Regon 891525310', { size: 7 , color: rgb(0.60, 0.70, 0.90) })
            });
        }
        const downloadPdf = ()=>{
            // const pdfBytes = modifyPdf(pdfUrl)
            modifyPdf(pdfUrl).then(()=>{
                const pdfBytes = outputUint8Array
                download(pdfBytes, "CMR-list-przewozowy.pdf", "application/pdf");
            })
        }
        const showPdf = ()=>{
            // const pdfBytes = modifyPdf(pdfUrl)
            isShown.value = true
            modifyPdf(pdfUrl).then(()=>{
                const pdfBytes = outputUint8Array
                const bytes  = new Uint8Array( pdfBytes ); 
                const blob   = new Blob( [ bytes ], { type: "application/pdf" } );
                const docUrl = URL.createObjectURL( blob );
                // document.getElementById('pdf').src = docUrl ;
                
                    
                var tabOrWindow = window.open(docUrl, '_blank');
                tabOrWindow.focus();
            })
        }
        const chooseClient = (index) =>{
            formClientIndex.value = index
        }
        const formWatcher = watch((formClientIndex), () => {
            if(formClientIndex.value != null){
                toggleDisableInputs(false)

                formConsigneeName.value = clients.value[formClientIndex.value].name
                formConsigneeStreet.value = clients.value[formClientIndex.value].streetAddress
                formConsigneeCity.value = clients.value[formClientIndex.value].zipCodeAddress + ' ' + clients.value[formClientIndex.value].cityAddress
                formConsigneeCountry.value = clients.value[formClientIndex.value].countryAddress
                formDestination.value = clients.value[formClientIndex.value].cityAddress + ', ' + clients.value[formClientIndex.value].countryAddress 
                formLoadingPlace.value = 'Zarow, ' + formSenderCountry.value + ', ' + moment().format('DD.MM.YYYY')
    
                formSpedCompany.value = shipment.value.forwarder.speditor
                formSpedCarPlates.value = shipment.value.forwarder.carPlates
                formSpedName.value = shipment.value.forwarder.firstName + ' ' + shipment.value.forwarder.lastName
                formGoodsQty.value = shipment.value.palletQty
            }
            
        })
        const showDomElementById = (id, flag)=>{
            let domElement = document.getElementById(id)
            if(domElement != null){
                domElement.disabled = flag
            }
        }

        const inputsWatcher = watchEffect(()=>{
            showDomElementById('inputLine3', !isLine3active.value)
            showDomElementById('inputLine4', !isLine4active.value)
            showDomElementById('inputLine5', !isLine5active.value)
            
        })

       const resetForm = ()=>{
         formClientIndex.value = null
         toggleDisableInputs345(true)
        // 1 - Sender
         formSenderName.value = 'Daicel Safety Systems Europe Sp z o.o.'
         formSenderStreet.value = 'ul. Sterefowa 6'
         formSenderCity.value = '58-130 Żarów'
         formSenderCountry.value ='PL'
        // 2 - Consignee
         formConsigneeName.value = ''
         formConsigneeStreet.value = ''
         formConsigneeCity.value = ''
         formConsigneeCountry.value = ''
        
         formDestination.value = ''
         formLoadingPlace.value = ''
         
        //6 Goods
         formGoodsMarks1.value = 'UN 3268 SAFETY DEVICES, CLASS 9, (E)'
         formGoodsMarks2.value = 'UN 3268 URZĄDZENIA BEZPIECZEŃSTWA, KLASA 9, (E)'
         isDgd.value = true
         
         toggleDisableInputs(true)
       }
       const toggleDisableInputs = (value)=>{
         let domElements = document.getElementsByClassName("dis")
                for (let index = 0; index < domElements.length; index++) {
                    domElements[index].disabled = value
                }
       }
       const toggleCheckbox = (x) =>{
        x = !x
       }
       
              
        onUnmounted(()=>{
            formWatcher()
            inputsWatcher()
        })
        const handleCalculateCbm = (newCbm)=>{
            if(newCbm == false){newCbm  = 0}
            else{
                formGoodsCBM.value = newCbm
            }
            showCalcFlag.value = false
        }
       

        return {
            chooseClient, resetForm,
            error, shipment, isPending,
                customers, customersError,
                moment, getData,
                clients, isShown,
                downloadPdf, showPdf,
                formClientIndex,
                formSenderName, formSenderStreet, formSenderCity, formSenderCountry,
                formConsigneeName, formConsigneeStreet, formConsigneeCity, formConsigneeCountry,
                formDestination, formAttachment1, formAttachment2,formLoadingPlace, 
                formGoodsMarks1, formGoodsMarks2, formGoodsMarks3,formGoodsMarks4, formGoodsMarks5, 
                isDgd,isAdrRegulated, isOverpack, isLine3active, isLine4active, isLine5active,
                formGoodsWeight, formGoodsCBM, formGoodsQty, formGoodsNet,
                formSpedCarPlates, formSpedCompany, formSpedName,
                handleShowCalc,
                handleCalculateCbm,
                showCalcFlag,
                toggleCheckbox

            }
    }

}
</script>

<style>
.no-btns{
    display: flex;
    justify-content: center;
    font-size:1.0vw;
    font-weight: 200;
    color: #a1ff6a;
    margin: 2vh 1vh;
    text-align: center;
}
.greenLabel{
    color: #a1ff6a;
}

.clients-icons-ctnr{
    display: flex;
}
.choose-clinet-head{
    font-size: 0.7vw;
    margin: 0 0 1vh 0;
    font-weight: 200;
}

.cmr-client-header{
    border-top: solid #fff 0.15vh;
    display: flex;
    justify-content: space-between;
    margin-top: 1.5vh;
}
.cmr-client-header .top-headers{
    border-top: none;
    margin-top:0;
    padding-top:0.5vh;
    font-size: 1.6vw;
}
.cmr-client-header .top-headers.yell{
    margin-left: 1vh;
    color: #ffbb00;
}
.client-icon{
    cursor: pointer;
    display: flex;
    flex-direction: row;
    width: 5vw;
    height: 4vh;
    background: linear-gradient(to right bottom, rgb(24, 76, 174), rgb(16, 41, 82));
    margin: 0.5vh 0.8vw 0.5vh 0;
    justify-content: center;
    align-items: center;
    border: solid #ffffff 0.15vh;
}
.client-icon:hover{
    background: linear-gradient(to right bottom, #ffbb00, #ad7f00);
    /* color: #222; */
    box-shadow: 0.5vh 0.5vh 0.8vh #000000a2;
    transform: scale(1.1);
    transition: 200ms ease-in-out;
}
.notChosenIcon{
    background: linear-gradient(to right bottom, rgb(89, 89, 89), rgb(58, 58, 58));
}
.client-icon p{
    font-size: 0.7vw;
}
.cmr-container{
    display: grid;
    grid-template-columns: 30vw 30vw ;
    gap: 2vw;
    margin: 2vh 2vw;
}

.cmr-form{
    display: flex;
    
    justify-content: center;
    
}
.cmr-form .reset{
    background: linear-gradient(to right bottom, #ff2727, #ae1a1a);
    color: #fff;
}
.cmr-form .reset:hover{
    background: linear-gradient(to right bottom, #ff9375, #e92b2b);

}

.cmr-form .cmr-form-set{
    display: grid;
    grid-template-columns: 10vw 18vw;
    gap: 1vw;
    /* justify-content: space-between; */
}
.cmr-form .cmr-form-set label{
    padding: 0.6vh 0;
    font-size: 0.7vw;
    font-weight: 200;
}
.cmr-form .cmr-form-set input, .cmr-form .cmr-form-set select{
    height: 2.3vh;
    width: 100%;
    padding-left: 0.7vw ;
    font-size: 1.2vh;
}
.cmr-form .cmrquest{
    font-size: 0.7vw;
    display: flex;
    flex-direction: column;
    margin: 4px auto;
    
}
.cmr-form .cmrquest .cmrquest-item label{
    padding-left: 10px;
    font-weight: 400;
}

.cmr-form .cmrquest .cmrquest-item input[type=checkbox]{
    transform: scale(1.6);    
    margin-top: 10px;
}
.cmr-form .linecheck{
    display: flex;
    justify-content: space-between;
}
.cmr-form .linecheck input{
    width: 20px;
    transform: scale(1.1);
}
.cmr-form-set .dim{
    display: grid;
    grid-template-columns: 1fr 1fr 1fr;
    gap: 1.1vw;
    
}
.cmr-form h3{
    border-top: solid 1px #fff;
    margin: 1.5vh 0 0.2vh 0;
    padding: 0.5vh 0vw 0.5vh 0;
    font-size: 0.8vw;
    
}
.cmr-form .cbm-btn{
    margin-bottom: 0;
    padding-bottom: 0;
    padding-right: 0;
    margin-right: 0;
}
.cmr-form .cbm-btn .btn{
    height: 2.3vh;
    padding: 0;
    padding-top: 4px;
    width: 1.6vw;
    color: #ffffff;
    font-weight: 500;
    background: none;
}
.notChosenCalc{
    display: none;
}

.cmr-form .cbm-btn .btn .material-symbols-outlined{
    font-size: 2.2vw;
    font-weight: 200;
}
.cmr-form .cbm-btn .btn:hover{
    /* background: #0c56aa; */
    transform: scale(1.3);
    box-shadow: 10px 10px 14px #00000050;
}

#pdf{
    display: flex;
    justify-content: center;
    align-self: center;
    margin: 0.5vh 0vh;
    padding: 0;
    width: 99%;
    height: 80vh;
    border: none;
    background: linear-gradient(to right bottom, #222, #555);
}
.clients-buttons-ctnr{
    display: flex;
    justify-content: center;
    align-items: center;
    /* flex-direction: column; */
    
}
.double-btns{
    
    display: flex;
    flex-direction: row;
    justify-content: space-around
    
}
.btn-cmr{
    width: 6vw;
    margin: 1vh;
    font-size: 0.6vw;
}
.alertfont{
    color: #fc9595;
}

</style>