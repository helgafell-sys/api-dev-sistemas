using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using Consultorio.Desktop.Models;

namespace Consultorio.Desktop.Services;

public sealed class ApiClient : IDisposable
{
    private readonly HttpClient _http;
    private readonly JsonSerializerOptions _jsonOptions;

    public ApiClient(string baseUrl)
    {
        _http = new HttpClient { BaseAddress = new Uri(baseUrl) };
        if (!_http.BaseAddress!.AbsoluteUri.EndsWith("/"))
            _http.BaseAddress = new Uri(_http.BaseAddress.AbsoluteUri + "/");

        _jsonOptions = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
    }

    public async Task<List<PatientDto>> GetAllAsync()
    {
        var resp = await _http.GetAsync("api/v1/patients");
        resp.EnsureSuccessStatusCode();
        var list = await resp.Content.ReadFromJsonAsync<List<PatientDto>>(_jsonOptions);
        return list ?? [];
    }

    public async Task<PatientDto?> GetByIdAsync(int id)
    {
        var resp = await _http.GetAsync($"api/v1/patients/{id}");
        if (resp.StatusCode == HttpStatusCode.NotFound) return null;
        resp.EnsureSuccessStatusCode();
        return await resp.Content.ReadFromJsonAsync<PatientDto>(_jsonOptions);
    }

    public async Task<(bool Success, string? Error, PatientDto? Created)> CreateAsync(PatientCreateDto createDto)
    {
        var resp = await _http.PostAsJsonAsync("api/v1/patients", createDto, _jsonOptions);
        if (resp.IsSuccessStatusCode)
        {
            var created = await resp.Content.ReadFromJsonAsync<PatientDto>(_jsonOptions);
            return (true, null, created);
        }
        var err = await ReadErrorAsync(resp);
        return (false, err, null);
    }

    public async Task<(bool Success, string? Error)> UpdateAsync(int id, PatientUpdateDto updateDto)
    {
        var resp = await _http.PutAsJsonAsync($"api/v1/patients/{id}", updateDto, _jsonOptions);
        if (resp.IsSuccessStatusCode || resp.StatusCode == HttpStatusCode.NoContent)
            return (true, null);
        var err = await ReadErrorAsync(resp);
        return (false, err);
    }

    public async Task<(bool Success, string? Error)> DeleteAsync(int id)
    {
        var resp = await _http.DeleteAsync($"api/v1/patients/{id}");
        if (resp.IsSuccessStatusCode || resp.StatusCode == HttpStatusCode.NoContent)
            return (true, null);
        var err = await ReadErrorAsync(resp);
        return (false, err);
    }

    private static async Task<string?> ReadErrorAsync(HttpResponseMessage resp)
    {
        try
        {
            var txt = await resp.Content.ReadAsStringAsync();
            return string.IsNullOrWhiteSpace(txt)
                ? $"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}"
                : $"HTTP {(int)resp.StatusCode} - {resp.ReasonPhrase}: {txt}";
        }
        catch
        {
            return $"HTTP {(int)resp.StatusCode}";
        }
    }

    public void Dispose() => _http.Dispose();
}