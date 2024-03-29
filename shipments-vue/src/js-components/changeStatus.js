import { ref } from 'vue'
import axios from 'axios'

const changeStatus = (url) =>{

    const error = ref(null)
    const isPending = ref(false)


    const change =  async (shipmentId, statusId) => {
        isPending.value = true

        var requestOptions = {
        method: 'PUT',
        headers: {
            'Accept':'*/*',
            'Content-Type':'application/json',
            'Access-Control-Allow-Origin': '*',
            'Access-Control-Allow-Headers': '*',
            'Access-Control-Allow-Credentials': 'true'
        },
        mode:'cors',
        };
      
        try {
            let resp = await axios.put( url + 'Shipment/'+shipmentId+'/'+ statusId, requestOptions)
            if (resp.status <200 & resp.status > 300){
                isPending.value = false

                throw Error('Coś poszło nie tak..')
            }
            isPending.value = false
            
        } catch (err) {
            isPending.value = false
            error.value = err.response.data
            console.log(error.value)
        }    
    }
      return {change, error, isPending}
}
export default changeStatus