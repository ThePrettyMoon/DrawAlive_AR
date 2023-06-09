# DrawAlive_AR
2023虚拟现实课课程作业

## Controlnet
### 环境配置：
首先创建一个新的conda环境并激活
conda env create -f environment.yaml

conda activate control

### 模型下载：
https://huggingface.co/lllyasviel/ControlNet/tree/main/models
把下载之后的模型放到路径下：ControlNet/annotator/ckpts

#### 图像处理：
python nettest.py
