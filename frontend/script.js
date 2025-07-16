document.addEventListener("DOMContentLoaded", () => {
  const originalUrlInput = document.getElementById("originalUrlInput");
  const shortenButton = document.getElementById("shortenButton");
  const resultDiv = document.getElementById("result");
  const shortenedLink = document.getElementById("shortenedLink");
  const errorMessage = document.getElementById("errorMessage");

  const API_BASE_URL = "http://localhost:5052";

  shortenButton.addEventListener("click", async () => {
    const originalUrl = originalUrlInput.value.trim();
    errorMessage.textContent = "";
    resultDiv.style.display = "none";

    if (!originalUrl) {
      errorMessage.textContent = "Por favor, digite um link para encurtar.";
      return;
    }

    try {
      const response = await fetch(`${API_BASE_URL}/api/url/shorten`, {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
          Accept: "application/json",
        },
        body: JSON.stringify({ url: originalUrl }),
      });

      if (!response.ok) {
        const errorData = await response.json();
        throw new Error(errorData.detail || "Error shortening link.");
      }

      const data = await response.json();
      const fullShortenedUrl = data.shortenedUrl;

      shortenedLink.href = fullShortenedUrl;
      shortenedLink.textContent = fullShortenedUrl;
      resultDiv.style.display = "block";
    } catch (error) {
      console.error("Erro:", error);
      errorMessage.textContent = `Erro: ${error.message}`;
    }
  });
});
