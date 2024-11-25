using Microsoft.ML.OnnxRuntime;
using Microsoft.ML.OnnxRuntime.Tensors;
using SkiaSharp;

namespace SmartAgricultureApp_MAUI;

public partial class PlantHealthPage : ContentPage
{
    private InferenceSession? session;
    private string? selectedImagePath;
    
    public PlantHealthPage()
    {
        InitializeComponent();
        
        // Load the ONNX model in the constructor
        string modelPath = @"C:\Users\harry\source\repos\ProjectPlantDatasetTrainingPyTorch\ProjectPlantDiseaseModel.onnx";

        try
        {
            // Verify the model file exists
            if (!File.Exists(modelPath))
            {
                throw new FileNotFoundException("ONNX model not found", modelPath);
            }

            // Load the ONNX model
            session = new InferenceSession(modelPath);

            // Display input names (for debugging purposes)
            foreach (var input in session.InputMetadata)
            {
                Console.WriteLine($"Input Name: {input.Key}, Type: {input.Value.ElementType}, Dimensions: {string.Join(",", input.Value.Dimensions)}");
            }
        }
        catch (Exception ex)
        {
            DisplayAlert("Error", $"Error loading model: {ex.Message}", "OK");
        }
    }
    
    private async void OnSelectedImageClicked(object sender, EventArgs e)
	{
		var result = await FilePicker.Default.PickAsync(new PickOptions
		{
			PickerTitle = "Select an image",
			FileTypes = FilePickerFileType.Images
		});

		if (result != null)
		{
			selectedImagePath = result.FullPath;
			SelectedImage.Source = ImageSource.FromFile(selectedImagePath);
		}
	}

	private float[] PreprocessImage(string imagePath)
	{
		using var imageStream = File.OpenRead(imagePath);
		using var inputImage = SKBitmap.Decode(imageStream);
		using var resizedImage = inputImage.Resize(new SKImageInfo(224, 224), SKFilterQuality.Medium);

		if (resizedImage == null)
			throw new Exception("Unable to resize image");

		var pixelData = new List<float>();

		for (int y = 0; y < resizedImage.Height; y++)
		{
			for (int x = 0; x < resizedImage.Width; x++)
			{
				var pixel = resizedImage.GetPixel(x, y);

				// Normalize pixel values to 0-1
				pixelData.Add(pixel.Red / 255f);
				pixelData.Add(pixel.Green / 255f);
				pixelData.Add(pixel.Blue / 255f);
			}
		}
		return pixelData.ToArray();
	}

	private async void OnPredictClicked(object sender, EventArgs e)
	{
		if (string.IsNullOrEmpty(selectedImagePath))
		{
			PredictionResult.Text = "Please select an image first.";
			return;
		}

		try
		{
			// Preprocess the selected image
			var imageFloats = PreprocessImage(selectedImagePath);

			// Prepare the input tensor
			var tensor = new DenseTensor<float>(imageFloats, new[] { 1, 3, 224, 224 });

			var inputs = new List<NamedOnnxValue>
			{
				NamedOnnxValue.CreateFromTensor("modelInput", tensor)
			};

			using var results = session.Run(inputs);

			// Parse the result and display it
			var resultTensor = results.First().AsEnumerable<float>().ToArray();
			var predictedClass = ParsePrediction(resultTensor);
			PredictionResult.Text = $"Predicted class: {predictedClass}";
		}
		catch (Exception ex)
		{
			await DisplayAlert("Error", $"Prediction failed: {ex.Message}", "OK");
		}
	}

	private string ParsePrediction(float[] scores)
	{
		var classNames = new[]
		{
			"Apple Apple Scab", "Apple Black Rot", "Apple Cedar Apple Rust", "Apple Healthy", "Blueberry Healthy",
			"Cherry Healthy", "Cherry Powdery Mildew", "Corn Cercospora Leaf Spot Gray Leaf Spot", "Corn Common Rust",
			"Corn Healthy", "Corn Northern Leaf Blight", "Grape Black Rot", "Grape Esca (Black Measles)", "Grape Healthy",
			"Grape Leaf Blight (Isariopsis Leaf Spot)", "Orange Haunglongbing (Citrus greening)", "Peach Bacterial Spot",
			"Peach Healthy", "Pepper Bell Bacterial Spot", "Pepper Bell Healthy", "Potato Early Blight", "Potato Healthy",
			"Potato Late Blight", "Raspberry Healthy", "Soybean Healthy", "Squash Powdery Mildew", "Strawberry Healthy",
			"Strawberry Leaf Scorch", "Tomato Bacterial Spot", "Tomato Early Blight", "Tomato Healthy", "Tomato Late Blight",
			"Tomato Leaf Mold", "Tomato Mosaic Virus", "Tomato Septoria Leaf Spot", "Tomato Spider Mites", "Tomato Target Spot",
			"Tomato Yellow Leaf Curl Virus"
		};

		int maxIndex = Array.IndexOf(scores, scores.Max());
		return classNames[maxIndex];
	}
}