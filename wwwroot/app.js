document.getElementById('url-form').addEventListener('submit', function (e) {
    e.preventDefault();

    const originalURL = document.getElementById('original-url').value;
    console.log('Original URL:', originalURL);

    fetch('/api/URL/shorten', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify(originalURL)
    })
        .then(response => {
            if (!response.ok) {
                throw new Error('Network response was not ok');
            }
            return response.json();
        })
        .then(data => {
            console.log('API Response:', data);
            const shortURL = data.shortURL;
            document.getElementById('short-url').textContent = shortURL;
            document.getElementById('short-url').href = shortURL; // Set the href attribute to make the link clickable
            document.getElementById('short-url-container').classList.remove('d-none');
        })
        .catch(error => {
            console.error('Error:', error);
            // Handle the error, e.g., display an error message to the user
            alert('An error occurred while shortening the URL. Please try again.');
        });
});
