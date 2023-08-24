const CANVAS_CELL_SIZE = 10; // pixels
const WALL_COLOR = 'rgb(0, 0, 0)';
const CELL_COLOR = 'rgb(175, 175, 175)';
const CELL_BORDER_COLOR = 'rgb(85, 85, 85)';
const DOOR_BORDER_COLOR = 'rgb(240, 230, 160)';
const DOOR_COLOR = 'rgb( 150, 140, 70)';

const fetchMap = async () => {
    console.log("Fetching an new map from the server. This might take a bit.");
    const response = await fetch('/api/map/generate', { headers: { Accept: 'application/json' } });

    if (response.ok) {
        console.log("New map received!");
        return response.json();
    }

    console.error(`Map generation call was unsuccessful: ${response.statusText}`);
    console.error(response.text());
}

const fetchAndRenderNewMap = async () => {
    const map = await fetchMap();

    if (!map) return;

    console.log(map);

    renderMap(map);
}

const renderMap = (map) => {
    const canvas = document.getElementById('map-canvas');

    if (!canvas.getContext) {
        console.error('Canvas context is not supported in this browser. Consider updating.');
        return
    }

    canvas.width = map.width * CANVAS_CELL_SIZE;
    canvas.height = map.height * CANVAS_CELL_SIZE;

    var ctx = canvas.getContext('2d');
    
    console.log("Drawing to canvas...");
    console.log(`  Map Size = ${map.width}, ${map.height}`);
    map.grid.forEach((column, x) => {
        column.forEach((region, y) => {
            if (region === 0) {
                ctx.fillStyle = WALL_COLOR;
                ctx.fillRect(x * CANVAS_CELL_SIZE, y * CANVAS_CELL_SIZE, CANVAS_CELL_SIZE, CANVAS_CELL_SIZE);
            } else {
                if (region === -3) {
                    ctx.strokeStyle = DOOR_BORDER_COLOR;
                    ctx.fillStyle = DOOR_COLOR;
                } else {
                    ctx.strokeStyle = CELL_BORDER_COLOR;
                    ctx.fillStyle = CELL_COLOR;
                }
                ctx.fillRect(x * CANVAS_CELL_SIZE, y * CANVAS_CELL_SIZE, CANVAS_CELL_SIZE, CANVAS_CELL_SIZE);
                ctx.strokeRect(x * CANVAS_CELL_SIZE, y * CANVAS_CELL_SIZE, CANVAS_CELL_SIZE, CANVAS_CELL_SIZE);
            }
        });
    });
}