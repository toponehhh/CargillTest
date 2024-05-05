import './App.css';
import Report from './components/report'

function App() {
    return (
        <div className='main'>
            <div className='mainheader'>
                <h1>Reconcillation Report</h1>
            </div>
            
            <div>
                <Report/>
            </div>
        </div>
    );
}

export default App;
