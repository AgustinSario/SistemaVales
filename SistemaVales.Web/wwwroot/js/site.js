// Please see documentation at https://learn.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
$(document).ready(function () {
    // Chatbot Elements
    const $container = $('#chatbot-container');
    const $toggle = $('#chatbot-toggle');
    const $close = $('#chatbot-close');
    const $form = $('#chatbot-form');
    const $input = $('#chatbot-input');
    const $messages = $('#chatbot-messages');

    // Toggle Chatbot
    $toggle.on('click', function () {
        $container.removeClass('chatbot-closed').addClass('chatbot-open');
        $input.focus();
    });

    $close.on('click', function () {
        $container.removeClass('chatbot-open').addClass('chatbot-closed');
    });

    // Send Message
    $form.on('submit', async function (e) {
        e.preventDefault();

        const text = $input.val().trim();
        if (!text) return;

        // Clear input
        $input.val('');

        // 1. Draw User Message
        appendUserMessage(text);

        // 2. Draw Loading Indicator
        const $loading = appendLoadingMessage();

        // 3. Fetch Data
        try {
            const response = await fetch(`/Expedientes/ChatbotConsultar?numero=${encodeURIComponent(text)}`);
            const result = await response.json();

            $loading.remove(); // Remove loading

            if (result.success) {
                appendBotCard(result.data);
            } else {
                appendBotMessage(`Lo siento, ocurrió un error: ${result.message}`);
            }
        } catch (error) {
            $loading.remove();
            appendBotMessage("Error de conexión. No pude comunicarme con el servidor.");
        }
    });

    function scrollToBottom() {
        $messages.scrollTop($messages[0].scrollHeight);
    }

    function appendUserMessage(text) {
        const html = `
            <div class="d-flex flex-column mb-5 align-items-end">
                <div class="d-flex align-items-center mb-1 gap-2">
                    <span class="text-muted fs-7 fw-bold">Tú</span>
                    <div class="bg-success rounded-circle d-flex align-items-center justify-content-center" style="width: 25px; height: 25px;">
                        <i class="bi bi-person text-white fs-7"></i>
                    </div>
                </div>
                <div class="user-message-bubble p-3 rounded text-dark shadow-sm" style="max-width: 85%;">
                    ${text}
                </div>
            </div>`;
        $messages.append(html);
        scrollToBottom();
    }

    function appendLoadingMessage() {
        const html = `
            <div class="d-flex flex-column mb-5 align-items-start bot-loading">
                <div class="d-flex align-items-center mb-1 gap-2">
                    <div class="bg-primary rounded-circle d-flex align-items-center justify-content-center" style="width: 25px; height: 25px;">
                        <i class="bi bi-robot text-white fs-7"></i>
                    </div>
                    <span class="text-muted fs-7 fw-bold">Asistente</span>
                </div>
                <div class="bot-message-bubble p-3 rounded shadow-sm d-flex gap-1" style="max-width: 85%;">
                    <div class="spinner-grow spinner-grow-sm text-primary" role="status"></div>
                    <div class="spinner-grow spinner-grow-sm text-primary" role="status" style="animation-delay: 0.2s"></div>
                    <div class="spinner-grow spinner-grow-sm text-primary" role="status" style="animation-delay: 0.4s"></div>
                </div>
            </div>`;
        const $el = $(html);
        $messages.append($el);
        scrollToBottom();
        return $el;
    }

    function appendBotMessage(text) {
        const html = `
            <div class="d-flex flex-column mb-5 align-items-start">
                <div class="d-flex align-items-center mb-1 gap-2">
                    <div class="bg-primary rounded-circle d-flex align-items-center justify-content-center" style="width: 25px; height: 25px;">
                        <i class="bi bi-robot text-white fs-7"></i>
                    </div>
                    <span class="text-muted fs-7 fw-bold">Asistente</span>
                </div>
                <div class="bot-message-bubble p-3 rounded shadow-sm text-danger fw-semibold border-danger" style="max-width: 85%;">
                    ${text}
                </div>
            </div>`;
        $messages.append(html);
        scrollToBottom();
    }

    function appendBotCard(data) {
        const extractoHtml = data.extracto ? `
            <div class="mb-2">
                <span class="text-muted d-block fs-8 fw-bold text-uppercase">Extracto</span>
                <span class="text-dark fs-7">${data.extracto}</span>
            </div>` : '';

        const html = `
            <div class="d-flex flex-column mb-5 align-items-start">
                <div class="d-flex align-items-center mb-1 gap-2">
                    <div class="bg-primary rounded-circle d-flex align-items-center justify-content-center" style="width: 25px; height: 25px;">
                        <i class="bi bi-robot text-white fs-7"></i>
                    </div>
                    <span class="text-muted fs-7 fw-bold">Asistente</span>
                </div>
                <div class="bot-message-bubble p-0 rounded shadow-sm overflow-hidden" style="max-width: 90%;">
                    <div class="bg-light-primary p-3 border-bottom border-primary border-dashed">
                        <span class="d-block text-primary fw-bold mb-1">Resultado Encontrado</span>
                        <span class="badge badge-success text-uppercase">${data.estado}</span>
                    </div>
                    <div class="p-3">
                        <div class="mb-2">
                            <span class="text-muted d-block fs-8 fw-bold text-uppercase">Ubicación</span>
                            <span class="text-dark fs-7 fw-semibold">${data.ubicacion}</span>
                        </div>
                        ${extractoHtml}
                        <div>
                            <span class="text-muted d-block fs-8 fw-bold text-uppercase">Último Movimiento</span>
                            <span class="text-dark fs-7"><i class="bi bi-calendar-check me-1"></i>${data.fecha}</span>
                        </div>
                    </div>
                </div>
            </div>`;
        $messages.append(html);
        scrollToBottom();
    }
});
