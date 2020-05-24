import React, { useState, useCallback } from 'react';
import { Button } from 'reactstrap';
import { fetchWeather } from '../../utils/apiCalls';

const Weather = () => {
  const [data, setData] = useState(null);
  const getWeather = useCallback( () => {
      fetchWeather().then(dt => {
        console.log(dt);
        setData(dt);
      });
    },
    [],
  );

  return (
    <div>
      <Button onClick={getWeather}>Call API</Button>
      {JSON.stringify(data)}
    </div>
  );
}
 
export default Weather;