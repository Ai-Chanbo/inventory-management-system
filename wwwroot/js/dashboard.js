(function () {
  const canvas = document.getElementById('categoryStockChart');
  if (!canvas || !window.categoryStockData || typeof Chart === 'undefined') {
    return;
  }

  const labels = window.categoryStockData.labels || [];
  const values = window.categoryStockData.values || [];

  new Chart(canvas, {
    type: 'bar',
    data: {
      labels,
      datasets: [{
        label: '現在庫',
        data: values,
        backgroundColor: [
          '#0d6efd',
          '#20c997',
          '#ffc107',
          '#6f42c1',
          '#0dcaf0',
          '#198754',
          '#fd7e14',
          '#6610f2',
          '#dc3545',
          '#6c757d',
          '#0b5ed7'
        ],
        borderRadius: 6
      }]
    },
    options: {
      responsive: true,
      maintainAspectRatio: false,
      plugins: {
        legend: {
          display: false
        }
      },
      scales: {
        y: {
          beginAtZero: true,
          ticks: {
            precision: 0
          }
        }
      }
    }
  });
})();
