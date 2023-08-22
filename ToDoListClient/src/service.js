import axios from 'axios';
axios.defaults.baseURL = 'https://todoserver-6h1q.onrender.com/';
axios.interceptors.response.use(
function(response){
return response;
},function(error){
  console.log("error occured:(");
}
);
export default {
  getTasks: async () => {
    const result = await axios.get('item/')
    return result.data;
  },
  addTask: async (name) => {
    const result = await axios.post('item/',{Name:name, IsComplete:false})
    return result.data;
  },
  setCompleted: async (id, isComplete) => {
    const result = await axios.put(`item/${id}/${isComplete}`)
    return result.data;
  },

  deleteTask: async (id) => {
    const result = await axios.delete(`item/${id}`)
  }
  
};
