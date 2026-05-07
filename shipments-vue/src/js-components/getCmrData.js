import { ref } from 'vue'
import axios from 'axios'

const getCmrData = (url) => {

    const cmrData = ref(null)
    const error = ref(null)
    const isPending = ref(false)

    const loadCmrData = async (shipmentId, customerId) => {
        isPending.value = true
        error.value = null
        cmrData.value = null

        try {
            let resp = await axios.get(url + 'CmrData', {
                headers: { 'Accept': '*/*' },
                params: { shipmentId, customerId }
            })
            cmrData.value = resp.data
            isPending.value = false
        } catch (err) {
            isPending.value = false
            if (err.response && err.response.status === 404) {
                cmrData.value = null
            } else {
                error.value = err.response ? err.response.data : err.message
            }
        }
    }

    return { loadCmrData, cmrData, error, isPending }
}

export default getCmrData
