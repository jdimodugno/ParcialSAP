import axios from 'axios';

export const fetchWeather = () => {
  return axios.get(`${process.env.REACT_APP_API_URL}/weather`)
    .then((res) => {
      console.log(res);
      return res.data;
    })
}