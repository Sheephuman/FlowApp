async function loadFlows() {
    const canvas = document.getElementById('flowCanvas');
    const ctx = canvas.getContext('2d');

    ctx.clearRect(0, 0, canvas.width, canvas.height);
    ctx.font = "14px sans-serif";

    try {
        const res = await fetch('/flows');
        const items = await res.json();

        if (items.length === 0) {
            ctx.fillText("Loading [読み込み中...]", 10, 20);
            return;
        }

        items.forEach(item => {
            const x = item.X ?? item.x;
            const y = item.Y ?? item.y;
            const label = item.Label ?? item.label;

            ctx.fillStyle = "lightblue";
            ctx.fillRect(x, y, 100, 50);
            ctx.strokeRect(x, y, 100, 50);

            ctx.fillStyle = "black";
            ctx.fillText(label, x + 10, y + 25);

            // 座標を矩形上に表示
            ctx.fillStyle = "red";
            ctx.font = "12px sans-serif";
            ctx.fillText(`(${x}, ${y})`, x + 10, y + 45); // 矩形下部に座標
        });

    } catch (e) {
        ctx.fillStyle = "red";
        ctx.fillText("Error fetching data", 10, 20);
    }
}

// 初回ロード
loadFlows();

// 1秒ごとに更新
setInterval(loadFlows, 1000);
