<template>
    <NavbarComponent/>
    <Spinner v-if="isPending" />
    <div class="frame">
        <div class="view-container">
            <div class="sub-page-header item-e ">
                <p>ZAMÓWIENIA</p>
            </div>
            <div class="item-a" >
                <router-link :to="{name:'PurchaseOrderView'}" class="item-container main-list-btn">
                    <p><span class="material-symbols-outlined">list_alt</span></p>
                </router-link>
            
                <div class="item-container main-add-btn" @click="openAddComponent">
                    <p><span class="material-symbols-outlined">add</span></p>
                </div>
            </div>
            
            <div class="item-v">
                <div v-if="order && !goToAddFlag">
                    <div class="add-container">
                        <form class="form-add add-single-form" @submit.prevent="handleSubmit">
                            <h2>Edytuj dane zamówienia</h2>
                            <div class="form-set">
                                <label>Numer zamówienia</label>
                                <input type="text" v-model="poNumberForm" required>
                            </div>
                            <div class="form-set">
                                <label>Kategoria</label>
                                <select v-model="categoryForm">
                                    <option value="Sample">Sample</option>
                                    <option value="Standard">Standard</option>
                                    <option value="Inne">Inne</option>
                                </select>
                            </div>
                            <div class="form-set">
                                <label>Data dostawy</label>
                                <input type="datetime-local" v-model="deliveryDateForm" required>
                            </div>
                                <label>Incoterms</label>
                                <select class="incoterms" v-if="!incotermsError" v-model="incotermIdForm">
                                    <option v-for="inco in incoterms" :key="inco.id" :value="inco.id">{{inco.shortName}} - {{inco.name}}</option>
                                </select>
                            <div class="form-set">
                                <label>Klient</label>
                                <select v-if="!customersError" v-model="customerIdForm">
                                    <option v-for="customer in customers" :key="customer.id" :value="customer.id">{{customer.shortName}} ({{customer.cityAddress}} - {{customer.countryAddress}})</option>
                                </select>
                            </div>
                            <div v-if="!editOrderError" id="add-btn-container">
                                <button>Zapisz</button>
                            </div>
                            <div v-if="loadOrderError || editOrderError || customersError || incotermsError" class="error" >
                                <p>{{loadOrderError}}</p>
                                <p>{{customersError}}</p>
                                <p>{{incotermsError}}</p>
                                <p>{{editOrderError}}</p>
                            </div>
                            <div v-if="createdFlag" class="success">
                                <p>Zamówienie zostało zapisane.</p>
                            </div>
                            
                        </form>
                    </div>


                </div>
                <div v-if="goToAddFlag">
                    <AddPurchaseOrder />
                </div>
            </div>
        </div>
    </div>
</template>

<script>
import { onBeforeMount, ref } from 'vue'
import getPurchaseOrderById from '../js-components/getPurchaseOrderById.js'
import NavbarComponent from '../components/NavbarComponent.vue'
import getIncoterms from '../js-components/getIncoterms.js'
import getAllCustomers from '../js-components/getAllCustomers.js'
import editPurchaseOrder from '../js-components/editPurchaseOrder.js'
import AddPurchaseOrder from '../components/AddPurchaseOrder.vue'
import moment from 'moment'
import { useLinksStore } from '../stores/linksStore.js'
import Spinner from '../components/SpinnerComponent.vue'
export default {
    props:['orderId'],
    components:{NavbarComponent, AddPurchaseOrder, Spinner},
    setup(props){
        const linksStore = useLinksStore()
        const {loadOrder, error:loadOrderError, order, isPending} = getPurchaseOrderById(linksStore.url)
        const poNumberForm = ref('')
        const categoryForm = ref('')
        const deliveryDateForm = ref()
        const incotermIdForm = ref(1)
        const customerIdForm = ref()
        const createdFlag = ref(false)
        const {loadIncoterms, incoterms, error:incotermsError} = getIncoterms(linksStore.url)
        const {loadAllCustomers, customers, error:customersError} = getAllCustomers(linksStore.url)
        const {editOrder, error:editOrderError} = editPurchaseOrder(linksStore.url)
        const goToAddFlag = ref(false)

        onBeforeMount(()=>{
            loadOrder(props.orderId).then(()=>{
                loadIncoterms().then(()=>{
                    let incoId = incoterms.value.find(x => x.shortName == order.value.incotermName).id
                    incotermIdForm.value = incoId
                    
                })
                loadAllCustomers().then(()=>{
                    let customerId = customers.value.find(x => x.name == order.value.customerName).id
                    customerIdForm.value = customerId
                });
                poNumberForm.value = order.value.poNumber
                categoryForm.value = order.value.category
                deliveryDateForm.value = moment(order.value.deliveryDate).format("YYYY-MM-DDTHH:mm")
            })
        })

         const handleSubmit = ()=>{
            const poData = {
                poNumber : poNumberForm.value,
                category : categoryForm.value,
                deliveryDate : deliveryDateForm.value,
                incotermId : incotermIdForm.value,
                customerId : customerIdForm.value
            }
            editOrder(props.orderId, poData).then(()=>{
                if(editOrderError.value == null){
                    createdFlag.value = true
                    setTimeout(()=>{createdFlag.value = false},5000)
                }else{
                    setTimeout(()=>{editOrderError.value = null},4000)
                }
            })
           
        }
        const openAddComponent = ()=>{
        goToAddFlag.value = true
    }

        return{
            isPending,
            handleSubmit,
            loadOrderError,
            order,
            poNumberForm, categoryForm, deliveryDateForm, incotermIdForm, customerIdForm, 
            createdFlag,
            incoterms, incotermsError,
            customers, customersError,
            editOrderError,
            goToAddFlag,
            openAddComponent
        }
    }
}
</script>

<style>

</style>