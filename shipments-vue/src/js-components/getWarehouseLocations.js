import { ref } from 'vue'
import axios from 'axios'

const getWarehouseLocations = (url) =>{

    const locations = ref([])
    const error = ref(null)
    const isPending = ref(false)

    const loadLocations = async () => {
        isPending.value = true

        try {
                let resp = await axios.get(url + 'WarehouseArea/', {
                    headers: {
                            'Accept':'*/*'
                    }
                })
                //console.log(resp)
                if (resp.status <200 & resp.status > 300){
                    isPending.value = false
                    throw Error('Coś poszło nie tak..')
                }
                
                locations.value = resp.data
                isPending.value = false
            
            } catch (er) {
                error.value = er.response.data
            }

      }

      return {loadLocations, error, locations, isPending}
}

export default getWarehouseLocations