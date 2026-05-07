import { ref } from 'vue'
import axios from 'axios'

const saveCmrData = (url) => {

    const savedData = ref(null)
    const error = ref(null)
    const isPending = ref(false)

    const saveCmr = async (cmrPayload) => {
        isPending.value = true
        error.value = null

        const requestOptions = {
            method: 'POST',
            headers: {
                'Accept': '*/*',
                'Content-Type': 'application/json',
                'Access-Control-Allow-Origin': '*',
                'Access-Control-Allow-Headers': '*',
                'Access-Control-Allow-Credentials': 'true'
            },
            mode: 'cors'
        }

        try {
            let resp = await axios.post(url + 'CmrData', cmrPayload, requestOptions)
            savedData.value = resp.data
            isPending.value = false
        } catch (err) {
            error.value = err.response ? err.response.data : err.message
            isPending.value = false
        }
    }

    return { saveCmr, savedData, error, isPending }
}

export default saveCmrData
